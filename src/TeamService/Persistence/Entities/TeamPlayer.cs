namespace TeamService.Persistence.Entities;

public sealed class TeamPlayer
{
    public Guid TeamId { get; set; }
    public Guid PlayerId { get; set; }
    
    public Team? Team { get; set; }
    public Player? Player { get; set; }
}