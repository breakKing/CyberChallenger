using Identity.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Common.Persistence.Configurations;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "identity");
        
        builder.HasMany("_roles")
            .WithMany("_users")
            .UsingEntity(typeof(UserRole));
    }
}