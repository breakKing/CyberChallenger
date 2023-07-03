using IdentityProvider.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Persistence.Configurations;

public sealed class ApplicationConfig : IEntityTypeConfiguration<Application>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("applications", "openid");
    }
}