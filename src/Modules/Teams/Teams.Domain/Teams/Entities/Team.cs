using Common.Domain.Primitives;
using Teams.Domain.Games.ValueObjects;
using Teams.Domain.Teams.ValueObjects;
using Teams.Domain.Users.ValueObjects;

namespace Teams.Domain.Teams.Entities;

public sealed class Team : AggregateRoot<TeamId>
{
    private readonly List<Member> _members = new();
    private readonly List<Invitation> _invitations = new();
    private readonly List<JoinRequest> _joinRequests = new();

    public string Name { get; private set; }
    public GameId GameId { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public IReadOnlyList<Member> Members => _members.AsReadOnly();
    public IReadOnlyList<Invitation> Invitations => _invitations.AsReadOnly();
    public IReadOnlyList<JoinRequest> JoinRequests => _joinRequests.AsReadOnly();

    public Team(
        string name,
        GameId gameId,
        string? description,
        UserId creatorUserId) : base(TeamId.Create())
    {
        Name = name;
        GameId = gameId;
        Description = description;

        _members.Add(new Member(Id, creatorUserId, MemberRole.Captain));
    }
}