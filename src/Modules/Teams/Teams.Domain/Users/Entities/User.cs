using Common.Domain.Primitives;
using Teams.Domain.Users.ValueObjects;

namespace Teams.Domain.Users.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public string Name { get; private set; }

    public User(string name) : base(UserId.Create())
    {
        Name = name;
    }
}