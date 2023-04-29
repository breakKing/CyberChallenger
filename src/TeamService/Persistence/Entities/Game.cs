using MassTransit;
using Shared.Infrastructure.RelationalDatabase.Entities;

namespace TeamService.Persistence.Entities;

public sealed class Game : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public Game()
    {
        Id = NewId.NextGuid();
    }
}