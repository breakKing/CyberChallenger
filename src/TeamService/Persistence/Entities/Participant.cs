using MassTransit;
using Shared.Infrastructure.RelationalDatabase.Entities;

namespace TeamService.Persistence.Entities;

public sealed class Participant : EntityBase
{
    public Guid Id { get; set; }
    public string Nickname { get; set; } = string.Empty;

    public ICollection<Team>? Teams { get; set; }

    public Participant()
    {
        Id = NewId.NextGuid();
    }
}