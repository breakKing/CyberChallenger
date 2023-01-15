using IdentityProviderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProviderService.Persistence.Configurations;

public sealed class RoleClaimConfig : IEntityTypeConfiguration<RoleClaim>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("role_claims", "identity");
    }
}