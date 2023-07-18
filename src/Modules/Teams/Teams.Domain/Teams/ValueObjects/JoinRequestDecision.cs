using Common.Domain.Primitives;

namespace Teams.Domain.Teams.ValueObjects;

public sealed record JoinRequestDecision : ValueObject<int>
{
    private JoinRequestDecision(int value) : base(value)
    {
    }

    public static readonly JoinRequestDecision Pending = new JoinRequestDecision(0);
    public static readonly JoinRequestDecision Rejected = new JoinRequestDecision(1);
    public static readonly JoinRequestDecision Accepted = new JoinRequestDecision(2);
}