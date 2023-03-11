using Shared.Infrastructure.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Persistence.Entities;

public sealed class MessageStatus : EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }

    /// <inheritdoc />
    public MessageStatus(int id, string name)
    {
        Id = id;
        Name = name;
        
        var now = DateTimeOffset.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
        DeletedAt = null;
    }
}