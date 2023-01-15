using Microsoft.AspNetCore.Identity;

namespace IdentityProviderService.Persistence.Entities;

public sealed class User : IdentityUser<Guid>
{
    public ICollection<Session>? Sessions { get; set; }
    public ICollection<Role>? Roles { get; set; }
}