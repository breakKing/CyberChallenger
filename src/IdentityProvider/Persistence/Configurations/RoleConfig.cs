using IdentityProvider.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Persistence.Configurations;

public sealed class RoleConfig : IEntityTypeConfiguration<Role>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles", "identity");
        
        builder.HasMany(r => r.Users)
            .WithMany(u => u.Roles)
            .UsingEntity<UserRole>();

        builder.HasQueryFilter(r => !r.IsDeleted);
    }
}