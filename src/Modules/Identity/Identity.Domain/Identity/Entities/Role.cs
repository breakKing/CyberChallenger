using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Identity.Entities;

public sealed class Role : IdentityRole<Guid>
{
    private readonly List<User> _users = new();

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

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();
}