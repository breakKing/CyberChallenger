using Shared.Infrastructure.EventSourcing.Base;

namespace Shared.Infrastructure.EventSourcing.Entities;

public sealed class ConsumerMessage : MessageEntityBase
{
    public string ConsumerName { get; set; } = string.Empty;
}