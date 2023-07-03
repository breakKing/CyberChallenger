using IdentityProvider.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Persistence.Configurations;

public sealed class AuthorizationConfig : IEntityTypeConfiguration<Authorization>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Authorization> builder)
    {
        builder.ToTable("authorizations", "openid");
    }
}