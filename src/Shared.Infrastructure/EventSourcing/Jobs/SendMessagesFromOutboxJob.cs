using System.Text;
using Confluent.Kafka;
using KafkaFlow;
using KafkaFlow.Producers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Shared.Infrastructure.EventSourcing.Persistence.Constants;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;
using Shared.Infrastructure.EventSourcing.Persistence.Specifications;
using Shared.Infrastructure.Persistence.Interfaces;

namespace Shared.Infrastructure.EventSourcing.Jobs;

public sealed class SendMessagesFromOutboxJob : IJob
{
    private readonly IGenericUnitOfWork _unitOfWork;
    private readonly IProducerAccessor _producerAccessor;
    private readonly ILogger<SendMessagesFromOutboxJob> _logger;

    public SendMessagesFromOutboxJob(IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();

        _unitOfWork = scope.ServiceProvider.GetRequiredService<IGenericUnitOfWork>();
        _producerAccessor = scope.ServiceProvider.GetRequiredService<IProducerAccessor>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<SendMessagesFromOutboxJob>>();
    }

    /// <inheritdoc />
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting job for message producing...");
        
        var messagesToProduceSpec = GetMessagesForProduceSpec.Build();
        
        var messagesToProduce = await _unitOfWork.Repository<ProducerMessage>()
            .GetManyAsync(messagesToProduceSpec, false, context.CancellationToken);

        if (messagesToProduce.Count == 0)
        {
            _logger.LogInformation("There are no messages ready to be produced");
            return;
        }

        foreach (var message in messagesToProduce)
        {
            try
            {
                await ProduceMessageAsync(message, context.CancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception has been thrown while producing message {@Message}", message);
            }
            
        }
        
        _logger.LogInformation("Job for message producing has been completed");
    }

    private async Task ProduceMessageAsync(ProducerMessage message, CancellationToken ct = default)
    {
        var producer = _producerAccessor[message.ProducerName];

        if (producer is null)
        {
            _logger.LogWarning("There is no producer for message with id {MessageId}", message.Id);
            return;
        }

        var headers = GetMessageHeaders(message);

        var result = await producer.ProduceAsync(
            message.TopicName,
            message.Key,
            message.Value,
            headers);

        if (result.Status != PersistenceStatus.Persisted)
        {
            _logger.LogWarning("Failed to produce message with id {MessageId}", message.Id);
            return;
        }
            
        message.StatusId = MessageStatusDefinitions.Produced;
        await _unitOfWork.StartTransactionAsync(ct);
        _unitOfWork.Repository<ProducerMessage>().UpdateOne(message);
        await _unitOfWork.CommitAsync(ct);
            
        _logger.LogInformation("Message with id {MessageId} has been successfully produced", message.Id);
    }

    private static MessageHeaders GetMessageHeaders(ProducerMessage message)
    {
        var headers = new MessageHeaders();

        if (message.Headers is null || message.Headers.Count == 0)
        {
            return headers;
        }

        foreach (var header in message.Headers)
        {
            headers.Add(header.Key, Encoding.Unicode.GetBytes(header.Value));
        }
        
        return headers;
    }
}