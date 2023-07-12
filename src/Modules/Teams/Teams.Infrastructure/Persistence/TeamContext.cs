using Microsoft.EntityFrameworkCore;

namespace Teams.Infrastructure.Persistence;

public sealed class TeamContext : DbContext
{
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