using Shared.Infrastructure.Persistence.Entities;

namespace TeamService.Persistence.Entities;

public sealed class TeamParticipant : EntityBase
{
    public Guid TeamId { get; set; }
    public Guid ParticipantId { get; set; }
    public int TeamRoleId { get; set; }
    
    public Team? Team { get; set; }
    public Participant? Participant { get; set; }
    public TeamRole? TeamRole { get; set; }
}