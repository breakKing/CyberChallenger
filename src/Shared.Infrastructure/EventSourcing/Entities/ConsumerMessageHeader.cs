using Shared.Infrastructure.EventSourcing.Base;

namespace Shared.Infrastructure.EventSourcing.Entities;

public sealed class ConsumerMessageHeader : MessageHeaderEntityBase
{
    public ConsumerMessage? Message { get; set; }
}