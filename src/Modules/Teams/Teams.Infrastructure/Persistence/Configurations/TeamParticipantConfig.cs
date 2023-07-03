using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Entities;

namespace Teams.Infrastructure.Persistence.Configurations;

public sealed class TeamParticipantConfig : IEntityTypeConfiguration<TeamParticipant>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TeamParticipant> builder)
    {
        builder.ToTable("teams_participants", "teams");

        builder.HasOne(tp => tp.Team)
            .WithMany()
            .HasForeignKey()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(tp => tp.Participant)
            .WithMany()
            .HasForeignKey()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex("team_id", "participant_id")
            .IsUnique();
    }
}