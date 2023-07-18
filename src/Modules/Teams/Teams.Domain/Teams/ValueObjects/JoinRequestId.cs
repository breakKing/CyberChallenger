using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Teams.ValueObjects;

public sealed record JoinRequestId : ValueObject<Guid>
{
    private JoinRequestId() : base(NewId.NextGuid())
    {
    }

    public static JoinRequestId Create() => new();
}