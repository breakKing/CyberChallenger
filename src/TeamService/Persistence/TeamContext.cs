using Microsoft.EntityFrameworkCore;
using TeamService.Persistence.Entities;

namespace TeamService.Persistence;

public sealed class TeamContext : DbContext
{
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<TeamParticipant> TeamParticipants => Set<TeamParticipant>();
    public DbSet<TeamRole> TeamsRoles => Set<TeamRole>();
    
    /// <inheritdoc />
    public TeamContext(DbContextOptions options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamContext).Assembly);
    }
}