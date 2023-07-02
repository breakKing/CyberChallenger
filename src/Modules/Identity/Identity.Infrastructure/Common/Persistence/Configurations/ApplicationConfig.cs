using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Common.Persistence.Configurations;

public sealed class ApplicationConfig : IEntityTypeConfiguration<Domain.Authorization.Entities.Application>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Authorization.Entities.Application> builder)
    {
        builder.ToTable("applications", "openid");
    }
}