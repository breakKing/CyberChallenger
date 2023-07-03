using IdentityProvider.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Persistence.Configurations;

public sealed class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("user_tokens", "identity");
    }
}