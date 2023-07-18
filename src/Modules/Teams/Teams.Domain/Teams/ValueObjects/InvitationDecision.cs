using Common.Domain.Primitives;

namespace Teams.Domain.Teams.ValueObjects;

public sealed record InvitationDecision : ValueObject<int>
{
    private InvitationDecision(int value) : base(value)
    {
        
    }
    
    public static InvitationDecision Pending => new InvitationDecision(0);
    public static InvitationDecision Rejected => new InvitationDecision(1);
    public static InvitationDecision Accepted => new InvitationDecision(2);
}