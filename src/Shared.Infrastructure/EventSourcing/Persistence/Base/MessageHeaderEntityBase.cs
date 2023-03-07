using Shared.Infrastructure.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Persistence.Base;

public abstract class MessageHeaderEntityBase : EntityBase
{
    public Guid MessageId { get; set; }
    public string Key { get; set; } = string.Empty;
    public byte[] Value { get; set; } = Array.Empty<byte>();
}