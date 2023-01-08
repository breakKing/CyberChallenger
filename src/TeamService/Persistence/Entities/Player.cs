using MassTransit;
using Shared.Infrastructure.Persistence.Entities;

namespace TeamService.Persistence.Entities;

public sealed class Player : EntityBase
{
    public Guid Id { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public Guid? TeamId { get; set; }
    
    public ICollection<Team>? Teams { get; set; }

    public Player()
    {
        Id = NewId.NextGuid();
    }
}