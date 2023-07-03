using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Entities;

public sealed class Game : AggregateRoot
{
    public string Name { get; private set; }
    
    public Game(string name) : base(NewId.NextGuid())
    {
        Name = name;
    }
}