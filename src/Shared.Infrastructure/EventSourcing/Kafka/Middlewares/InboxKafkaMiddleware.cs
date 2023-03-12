using System.Globalization;
using System.Text;
using KafkaFlow;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.EventSourcing.Kafka.Extensions;
using Shared.Infrastructure.EventSourcing.Persistence.Constants;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;
using Shared.Infrastructure.EventSourcing.Persistence.Specifications;
using Shared.Infrastructure.Persistence.Interfaces;

namespace Shared.Infrastructure.EventSourcing.Kafka.Middlewares;

public sealed class InboxKafkaMiddleware : IMessageMiddleware
{
    private readonly IGenericUnitOfWork _unitOfWork;
    private readonly ILogger<InboxKafkaMiddleware> _logger;

    public InboxKafkaMiddleware(IGenericUnitOfWork unitOfWork, ILogger<InboxKafkaMiddleware> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        if (await MessageWasConsumedEarlierAsync(context))
        {
            _logger.LogInformation("Fetched message has already been consumed: {@Message}", context.Message);
            return;
        }

        await _unitOfWork.StartTransactionAsync();
        await next(context);
        StoreMessageAsConsumed(context);
        await _unitOfWork.CommitAsync();
    }

    private async Task<bool> MessageWasConsumedEarlierAsync(IMessageContext context)
    {
        var producedAtHeader = context.Headers.GetString(HeaderDefinitions.ProducedAt);
        var messageTypeHeader = context.Headers.GetString(HeaderDefinitions.MessageType);

        if (string.IsNullOrWhiteSpace(producedAtHeader))
        {
            _logger.LogWarning(
                "There has been a message without 'produced_at' header: {@Message}", context.Message);
            
            return true;
        }
        
        if (string.IsNullOrWhiteSpace(messageTypeHeader))
        {
            _logger.LogWarning(
                "There has been a message without 'message_type' header: {@Message}", context.Message);
            
            return true;
        }

        var producedAt = DateTimeOffset.Parse(producedAtHeader, CultureInfo.InvariantCulture);

        var spec = GetConsumedMessagesSpec.Build(
            context.ConsumerContext.ConsumerName,
            context.ConsumerContext.Topic,
            messageTypeHeader,
            context.GetMessageKey(),
            context.GetMessageValue(),
            producedAt);

        var alreadyConsumed = await _unitOfWork.Repository<ConsumerMessage>().HasAnyAsync(spec);

        return alreadyConsumed;
    }

    private void StoreMessageAsConsumed(IMessageContext context)
    {
        var message = new ConsumerMessage
        {
            Type = context.Headers.GetString(HeaderDefinitions.MessageType)!,
            Key = context.GetMessageKey(),
            Value = context.GetMessageValue(),
            TopicName = context.ConsumerContext.Topic,
            StatusId = MessageStatusDefinitions.Consumed,
            ConsumerName = context.ConsumerContext.ConsumerName
        };

        message.Headers = context.Headers.Select(h => new ConsumerMessageHeader
        {
            Key = h.Key,
            Value = Encoding.Default.GetString(h.Value),
            MessageId = message.Id
        }).ToList();

        _unitOfWork.Repository<ConsumerMessage>().AddOne(message);
    }
}