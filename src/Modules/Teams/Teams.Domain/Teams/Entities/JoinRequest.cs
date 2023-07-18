using Common.Domain.Primitives;
using Teams.Domain.Teams.ValueObjects;
using Teams.Domain.Users.ValueObjects;

namespace Teams.Domain.Teams.Entities;

public sealed class JoinRequest : Entity<JoinRequestId>
{
    public TeamId TeamId { get; private set; }
    public UserId RequesterUserId { get; private set; }
    public string? RequesterMessage { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public MemberId? DeciderMemberId { get; private set; }
    public DateTimeOffset? DecidedAt { get; private set; }
    public JoinRequestDecision Decision { get; private set; } = JoinRequestDecision.Pending;
    public string? DecisionMessage { get; private set; }
    
    /// <inheritdoc />
    public JoinRequest(
        TeamId teamId, 
        UserId requesterUserId, 
        string? requesterMessage = null) : base(JoinRequestId.Create())
    {
        TeamId = teamId;
        RequesterUserId = requesterUserId;
        RequesterMessage = requesterMessage;
    }
}