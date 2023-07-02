using Identity.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Common.Persistence.Configurations;

public sealed class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("user_tokens", "identity");
    }
}