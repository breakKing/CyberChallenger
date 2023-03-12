using MassTransit;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;
using Shared.Infrastructure.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Persistence.Base;

public abstract class MessageEntityBase : EntityBase
{
    public Guid Id { get; set; } = NewId.NextGuid();
    public string Type { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string TopicName { get; set; } = string.Empty;
    public int StatusId { get; set; }
    
    public MessageStatus? Status { get; set; }
}