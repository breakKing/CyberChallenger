using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Persistence.Entities;

namespace TeamService.Persistence.Configurations;

public sealed class ParticipantConfig : IEntityTypeConfiguration<Participant>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.ToTable("participants", "teams");

        builder.HasMany(p => p.Teams)
            .WithMany(t => t.Participants)
            .UsingEntity<TeamParticipant>();

        builder.HasIndex(p => p.Nickname)
            .IsUnique();
    }
}