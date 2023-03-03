using Shared.Infrastructure.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Entities;

public sealed class ProducerMessageHeader : EntityBase
{
    public Guid MessageId { get; set; }
    public string Key { get; set; } = string.Empty;
    public byte[] Value { get; set; } = Array.Empty<byte>();
    
    public ProducerMessage? Message { get; set; }
}