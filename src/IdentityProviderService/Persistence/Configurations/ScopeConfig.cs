using IdentityProviderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProviderService.Persistence.Configurations;

public sealed class ScopeConfig : IEntityTypeConfiguration<Scope>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Scope> builder)
    {
        builder.ToTable("scopes", "openid");
    }
}