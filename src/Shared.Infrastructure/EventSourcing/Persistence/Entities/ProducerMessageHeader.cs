using Shared.Infrastructure.EventSourcing.Persistence.Base;

namespace Shared.Infrastructure.EventSourcing.Persistence.Entities;

public sealed class ProducerMessageHeader : MessageHeaderEntityBase
{
    public ProducerMessage? Message { get; set; }
}