using Shared.Infrastructure.EventSourcing.Base;

namespace Shared.Infrastructure.EventSourcing.Entities;

public sealed class ProducerMessage : MessageEntityBase
{
    public string ProducerName { get; set; } = string.Empty;

    public ICollection<ProducerMessageHeader>? Headers { get; set; }
}