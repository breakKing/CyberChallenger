using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Persistence.Entities;

namespace TeamService.Persistence.Configurations;

public sealed class TeamPlayerConfig : IEntityTypeConfiguration<TeamPlayer>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TeamPlayer> builder)
    {
        builder.ToTable("teams_players", "teams");

        builder.HasKey(tp => new { tp.TeamId, tp.PlayerId });

        builder.HasOne(tp => tp.Team)
            .WithMany()
            .HasForeignKey(tp => tp.TeamId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(tp => tp.Player)
            .WithMany()
            .HasForeignKey(tp => tp.PlayerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tp => tp.TeamRole)
            .WithMany()
            .HasForeignKey(tp => tp.TeamRoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}