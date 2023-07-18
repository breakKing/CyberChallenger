using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Users.ValueObjects;

public sealed record UserId : ValueObject<Guid>
{
    private UserId() : base(NewId.NextGuid())
    {
        
    }

    public static UserId Create() => new();
}