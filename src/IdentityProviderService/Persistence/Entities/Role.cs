using Microsoft.AspNetCore.Identity;

namespace IdentityProviderService.Persistence.Entities;

public sealed class Role : IdentityRole<Guid>
{
    public ICollection<User>? Users { get; set; }
}