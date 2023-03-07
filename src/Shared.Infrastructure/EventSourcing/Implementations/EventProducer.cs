using System.Text.Json;
using Shared.Infrastructure.EventSourcing.Constants;
using Shared.Infrastructure.EventSourcing.Entities;
using Shared.Infrastructure.EventSourcing.Interfaces;
using Shared.Infrastructure.Persistence.Interfaces;

namespace Shared.Infrastructure.EventSourcing.Implementations;

public sealed class EventProducer : IEventProducer
{
    private readonly IGenericUnitOfWork _unitOfWork;

    public EventProducer(IGenericUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task ProduceAsync<TMessage>(string producerName, string topic, string key, TMessage message,
        IDictionary<string, byte[]>? headers = null, CancellationToken ct = default)
    {
        var messageToProduce = new ProducerMessage
        {
            Key = key,
            Body = JsonSerializer.SerializeToDocument(message),
            TopicName = topic,
            StatusId = MessageStatusesDefinition.ReadyToBeProduced,
            ProducerName = producerName,
        };

        if (headers is not null)
        {
            messageToProduce.Headers = headers.Select(h => new ProducerMessageHeader
            {
                Key = h.Key,
                Value = h.Value,
                MessageId = messageToProduce.Id
            }).ToList();
        }

        await _unitOfWork.StartTransactionAsync(ct);
        _unitOfWork.Repository<ProducerMessage>().AddOne(messageToProduce);
        await _unitOfWork.CommitAsync(ct);
    }
}