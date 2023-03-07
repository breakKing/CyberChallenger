using Shared.Infrastructure.EventSourcing.Persistence.Base;

namespace Shared.Infrastructure.EventSourcing.Persistence.Entities;

public sealed class ConsumerMessageHeader : MessageHeaderEntityBase
{
    public ConsumerMessage? Message { get; set; }
}