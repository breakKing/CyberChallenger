using Identity.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Common.Persistence.Configurations;

public sealed class RoleClaimConfig : IEntityTypeConfiguration<RoleClaim>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("role_claims", "identity");
    }
}