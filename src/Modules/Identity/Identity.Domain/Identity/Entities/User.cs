using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Identity.Entities;

public sealed class User : IdentityUser<Guid>
{
    private readonly List<Role> _roles = new();
    
    public User()
    {
        Id = NewId.NextGuid();
    }

    public User(Guid id, string userName) : base(userName)
    {
        Id = id;
    }

    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    public void AddRole(Role role)
    {
        if (_roles.Exists(r => r.Id == role.Id))
        {
            return;
        }
        
        _roles.Add(role);
    }
}