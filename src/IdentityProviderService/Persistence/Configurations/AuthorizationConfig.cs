using IdentityProviderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProviderService.Persistence.Configurations;

public sealed class AuthorizationConfig : IEntityTypeConfiguration<Authorization>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Authorization> builder)
    {
        builder.ToTable("authorizations", "openid");
    }
}