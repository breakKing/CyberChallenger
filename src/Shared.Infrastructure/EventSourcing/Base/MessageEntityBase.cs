using MassTransit;
using Shared.Infrastructure.EventSourcing.Entities;
using Shared.Infrastructure.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Base;

public abstract class MessageEntityBase : EntityBase
{
    public Guid Id { get; set; } = NewId.NextGuid();
    public string Key { get; set; } = string.Empty;
    public object Body { get; set; } = new();
    public string TopicName { get; set; } = string.Empty;
    public int StatusId { get; set; }
    
    public MessageStatus? Status { get; set; }
}