using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Identity;

public sealed class User : IdentityUser<Guid>
{
    private readonly List<Role> _roles = new();
    public User()
    {
        Id = NewId.NextGuid();
    }

    internal User(Guid id, string userName) : base(userName)
    {
        Id = id;
    }

    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
}