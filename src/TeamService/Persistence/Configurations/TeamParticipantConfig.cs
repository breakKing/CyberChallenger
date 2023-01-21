using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Persistence.Entities;

namespace TeamService.Persistence.Configurations;

public sealed class TeamParticipantConfig : IEntityTypeConfiguration<TeamParticipant>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TeamParticipant> builder)
    {
        builder.ToTable("teams_participants", "teams");

        builder.HasKey(tp => new { tp.TeamId, PlayerId = tp.ParticipantId });

        builder.HasOne(tp => tp.Team)
            .WithMany()
            .HasForeignKey(tp => tp.TeamId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(tp => tp.Participant)
            .WithMany()
            .HasForeignKey(tp => tp.ParticipantId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tp => tp.TeamRole)
            .WithMany()
            .HasForeignKey(tp => tp.TeamRoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}