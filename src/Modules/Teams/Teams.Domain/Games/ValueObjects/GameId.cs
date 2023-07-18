using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Games.ValueObjects;

public sealed record GameId : ValueObject<Guid>
{
    private GameId() : base(NewId.NextGuid())
    {
        
    }

    public static GameId Create() => new();
}