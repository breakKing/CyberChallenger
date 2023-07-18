using Common.Domain.Primitives;

namespace Teams.Domain.Teams.ValueObjects;

public sealed record MemberRole : ValueObject<int>
{
    private MemberRole(int value) : base(value)
    {
        
    }

    public static readonly MemberRole Player = new MemberRole(0);
    public static readonly MemberRole Captain = new MemberRole(1);
    public static readonly MemberRole Coach = new MemberRole(2);
    public static readonly MemberRole Manager = new MemberRole(3);
}