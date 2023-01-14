using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Persistence.Constants;
using TeamService.Persistence.Entities;

namespace TeamService.Persistence.Configurations;

public sealed class TeamRoleConfig : IEntityTypeConfiguration<TeamRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TeamRole> builder)
    {
        builder.ToTable("team_roles", "teams");

        builder.HasData(TeamRolesDefinitions.FullList);
    }
}