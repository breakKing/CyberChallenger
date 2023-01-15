using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace IdentityProviderService.Persistence.Entities;

public sealed class Role : IdentityRole<Guid>
{
    public Role()
    {
        Id = NewId.NextGuid();
    }
    
    public bool IsDeleted { get; set; }
    
    public ICollection<User>? Users { get; set; }
}