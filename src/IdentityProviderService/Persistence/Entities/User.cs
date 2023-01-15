using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace IdentityProviderService.Persistence.Entities;

public sealed class User : IdentityUser<Guid>
{
    public User()
    {
        Id = NewId.NextGuid();
    }
    
    public bool IsDeleted { get; set; }
    
    public ICollection<Session>? Sessions { get; set; }
    public ICollection<Role>? Roles { get; set; }
}