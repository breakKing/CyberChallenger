using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Persistence.Entities;

public sealed class User : IdentityUser<Guid>
{
    public User()
    {
        Id = NewId.NextGuid();
    }

    internal User(Guid id, string userName) : base(userName)
    {
        Id = id;
    }
    
    public bool IsDeleted { get; set; }
    
    public ICollection<Role>? Roles { get; set; }
}