using IdentityProvider.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Persistence.Configurations;

public sealed class UserClaimConfig : IEntityTypeConfiguration<UserClaim>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable("user_claims", "identity");
    }
}