using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Persistence.Entities;

namespace TeamService.Persistence.Configurations;

public sealed class PlayerConfig : IEntityTypeConfiguration<Player>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players", "teams");

        builder.HasMany(p => p.Teams)
            .WithMany(t => t.Players)
            .UsingEntity<TeamPlayer>();

        builder.HasIndex(p => p.Nickname)
            .IsUnique();
    }
}