using IdentityProviderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProviderService.Persistence.Configurations;

public sealed class TokenConfig : IEntityTypeConfiguration<Token>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.ToTable("tokens", "openid");
    }
}