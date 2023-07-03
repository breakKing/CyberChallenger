using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Persistence.Entities;

public sealed class Role : IdentityRole<Guid>
{
    public Role()
    {
        Id = NewId.NextGuid();
    }

    public Role(string name) : this()
    {
        Name = name;
    }

    internal Role(Guid id, string name) : this(name)
    {
        Id = id;
    }

    public bool IsDeleted { get; set; }
    
    public ICollection<User>? Users { get; set; }
}