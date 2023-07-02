using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Common.Persistence.Configurations;

public sealed class AuthorizationConfig : IEntityTypeConfiguration<Domain.Authorization.Entities.Authorization>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Authorization.Entities.Authorization> builder)
    {
        builder.ToTable("authorizations", "openid");
    }
}