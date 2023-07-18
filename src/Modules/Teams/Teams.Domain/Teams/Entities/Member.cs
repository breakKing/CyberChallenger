using Common.Domain.Primitives;
using Teams.Domain.Teams.ValueObjects;
using Teams.Domain.Users.ValueObjects;

namespace Teams.Domain.Teams.Entities;

public sealed class Member : Entity<MemberId>
{
    public TeamId TeamId { get; private set; }
    public UserId UserId { get; private set; }
    public MemberRole Role { get; private set; }
    public DateTimeOffset JoinedAt { get; private set; } = DateTimeOffset.UtcNow;

    public Member(
        TeamId teamId,
        UserId userId,
        MemberRole role) : base(MemberId.Create())
    {
        TeamId = teamId;
        UserId = userId;
        Role = role;
    }
}