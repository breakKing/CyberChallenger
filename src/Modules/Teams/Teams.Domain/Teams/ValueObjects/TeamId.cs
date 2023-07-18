using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Teams.ValueObjects;

public sealed record TeamId : ValueObject<Guid>
{
    private TeamId() : base(NewId.NextGuid())
    {
        
    }

    public static TeamId Create() => new();
};