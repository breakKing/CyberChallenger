using MassTransit;
using Shared.Infrastructure.Persistence.Entities;

namespace TeamService.Persistence.Entities;

public sealed class Team : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid GameId { get; set; }
    
    public ICollection<Player>? Players { get; set; }

    public Team()
    {
        Id = NewId.NextGuid();
    }
}