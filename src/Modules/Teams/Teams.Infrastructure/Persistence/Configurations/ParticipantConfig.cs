using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Entities;

namespace Teams.Infrastructure.Persistence.Configurations;

public sealed class ParticipantConfig : IEntityTypeConfiguration<Participant>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.ToTable("participants", "teams");

        builder.Navigation("_teams")
            .AutoInclude();

        builder.HasMany("_teams")
            .WithMany("_participants")
            .UsingEntity(typeof(TeamParticipant));

        builder.HasIndex(p => p.Nickname)
            .IsUnique();
    }
}