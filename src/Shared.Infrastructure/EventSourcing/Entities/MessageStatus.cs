using Shared.Infrastructure.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Entities;

public sealed class MessageStatus : EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }

    /// <inheritdoc />
    public MessageStatus(int id, string name)
    {
        Id = id;
        Name = name;
    }
}