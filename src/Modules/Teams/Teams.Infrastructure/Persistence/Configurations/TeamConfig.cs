using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Entities;

namespace Teams.Infrastructure.Persistence.Configurations;

public sealed class TeamConfig : IEntityTypeConfiguration<Team>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("teams", "teams");
        
        builder.Navigation("_participants")
            .AutoInclude();

        builder.HasMany("_participants")
            .WithMany("_teams")
            .UsingEntity(typeof(TeamParticipant));

        builder.HasOne(t => t.Game)
            .WithMany()
            .HasForeignKey()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}