using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Teams.ValueObjects;

public sealed record MemberId : ValueObject<Guid>
{
    private MemberId() : base(NewId.NextGuid())
    {
        
    }

    public static MemberId Create() => new();
}