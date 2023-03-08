using System.Globalization;
using System.Text.Json;
using Shared.Infrastructure.EventSourcing.Persistence.Constants;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;
using Shared.Infrastructure.EventSourcing.Services.Interfaces;
using Shared.Infrastructure.Persistence.Interfaces;

namespace Shared.Infrastructure.EventSourcing.Services.Implementations;

public sealed class EventProducer : IEventProducer
{
    private readonly IGenericUnitOfWork _unitOfWork;

    public EventProducer(IGenericUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public Task ProduceAsync<TMessage>(string producerName, string topic, string key, TMessage message,
        IDictionary<string, byte[]>? headers = null, CancellationToken ct = default)
    {
        var messageToProduce = new ProducerMessage
        {
            Key = key,
            Value = JsonSerializer.Serialize(message),
            TopicName = topic,
            StatusId = MessageStatusDefinitions.ReadyToBeProduced,
            ProducerName = producerName,
        };

        var headersList = new List<ProducerMessageHeader>()
        {
            new()
            {
                Key = HeaderDefinitions.ProducedAt,
                Value = DateTimeOffset.UtcNow.ToString(CultureInfo.InvariantCulture),
                MessageId = messageToProduce.Id
            }
        };

        if (headers is not null)
        {
            headersList.AddRange(headers.Select(h => new ProducerMessageHeader
            {
                Key = h.Key,
                Value = h.Value.ToString() ?? string.Empty,
                MessageId = messageToProduce.Id
            }));
        }

        messageToProduce.Headers = headersList;

        _unitOfWork.Repository<ProducerMessage>().AddOne(messageToProduce);

        return Task.CompletedTask;
    }
}