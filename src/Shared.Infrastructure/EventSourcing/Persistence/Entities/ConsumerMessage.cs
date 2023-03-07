using Shared.Infrastructure.EventSourcing.Persistence.Base;

namespace Shared.Infrastructure.EventSourcing.Persistence.Entities;

public sealed class ConsumerMessage : MessageEntityBase
{
    public string ConsumerName { get; set; } = string.Empty;
    
    public ICollection<ConsumerMessageHeader>? Headers { get; set; }
}