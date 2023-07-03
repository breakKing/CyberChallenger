using Common.Domain.Primitives;
using MassTransit;
using Teams.Domain.Constants;

namespace Teams.Domain.Entities;

public sealed class TeamParticipant : Entity
{
    public Team Team { get; private set; }
    public Participant Participant { get; private set; }
    public TeamRole TeamRole { get; private set; }

    public TeamParticipant(
        Team team, 
        Participant participant, 
        TeamRole teamRole = TeamRole.Player) : base(NewId.NextGuid())
    {
        Team = team;
        Participant = participant;
        TeamRole = teamRole;
    }
}