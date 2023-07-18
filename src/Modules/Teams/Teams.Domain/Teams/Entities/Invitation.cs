using Common.Domain.Primitives;
using Teams.Domain.Teams.ValueObjects;
using Teams.Domain.Users.ValueObjects;

namespace Teams.Domain.Teams.Entities;

public sealed class Invitation : Entity<InvitationId>
{
    public TeamId TeamId { get; private set; }
    public MemberId InvitorMemberId { get; private set; }
    public UserId InviteeUserId { get; private set; }
    public string? InvitorMessage { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DecidedAt { get; private set; }
    public InvitationDecision Decision { get; private set; } = InvitationDecision.Pending;
    public string? DecisionMessage { get; private set; }
    
    public Invitation(
        TeamId teamId, 
        MemberId invitorMemberId, 
        UserId inviteeUserId, 
        string? invitorMessage) : base(InvitationId.Create())
    {
        TeamId = teamId;
        InvitorMemberId = invitorMemberId;
        InviteeUserId = inviteeUserId;
        InvitorMessage = invitorMessage;
    }
}