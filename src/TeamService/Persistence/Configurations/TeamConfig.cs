using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Persistence.Entities;

namespace TeamService.Persistence.Configurations;

public sealed class TeamConfig : IEntityTypeConfiguration<Team>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("teams", "teams");

        builder.HasMany(t => t.Players)
            .WithMany(p => p.Teams)
            .UsingEntity<TeamPlayer>();

        builder.Property(t => t.GameId)
            .IsRequired();
    }
}