using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Teams.ValueObjects;

public sealed record InvitationId : ValueObject<Guid>
{
    private InvitationId() : base(NewId.NextGuid())
    {
    }

    public static InvitationId Create() => new();
}