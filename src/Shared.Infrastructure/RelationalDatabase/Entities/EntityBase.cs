using System.ComponentModel.DataAnnotations;

namespace Shared.Infrastructure.RelationalDatabase.Entities;

public abstract class EntityBase
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    [Timestamp]
    public uint Version { get; set; }
}