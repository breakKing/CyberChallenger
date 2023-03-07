using Shared.Infrastructure.EventSourcing.Persistence.Base;

namespace Shared.Infrastructure.EventSourcing.Persistence.Entities;

public sealed class ProducerMessage : MessageEntityBase
{
    public string ProducerName { get; set; } = string.Empty;

    public ICollection<ProducerMessageHeader>? Headers { get; set; }
}