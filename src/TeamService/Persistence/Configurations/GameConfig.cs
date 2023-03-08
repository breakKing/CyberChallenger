using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Persistence.Entities;

namespace TeamService.Persistence.Configurations;

public sealed class GameConfig : IEntityTypeConfiguration<Game>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games", "games");

        builder.HasIndex(g => g.Name)
            .IsUnique();
    }
}