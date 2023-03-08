using MassTransit;
using Shared.Infrastructure.Persistence.Entities;

namespace TeamService.Persistence.Entities;

public sealed class Team : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid GameId { get; set; }
    
    public Game? Game { get; set; }
    public ICollection<Participant>? Participants { get; set; }

    public Team()
    {
        Id = NewId.NextGuid();
    }

    public Team(string name, Guid gameId) : this()
    {
        Name = name;
        GameId = gameId;
    }
}