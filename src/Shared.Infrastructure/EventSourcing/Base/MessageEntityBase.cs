using System.Text.Json;
using MassTransit;
using Shared.Infrastructure.EventSourcing.Entities;
using Shared.Infrastructure.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Base;

public abstract class MessageEntityBase : EntityBase
{
    public Guid Id { get; set; } = NewId.NextGuid();
    public string Key { get; set; } = string.Empty;
    public JsonDocument Body { get; set; } = JsonDocument.Parse(string.Empty);
    public string TopicName { get; set; } = string.Empty;
    public int StatusId { get; set; }
    
    public MessageStatus? Status { get; set; }
}