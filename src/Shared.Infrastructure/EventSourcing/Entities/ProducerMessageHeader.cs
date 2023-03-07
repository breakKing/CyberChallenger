using Shared.Infrastructure.EventSourcing.Base;

namespace Shared.Infrastructure.EventSourcing.Entities;

public sealed class ProducerMessageHeader : MessageHeaderEntityBase
{
    public ProducerMessage? Message { get; set; }
}