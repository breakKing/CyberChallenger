using Common.Domain.Primitives;
using Teams.Domain.Games.ValueObjects;

namespace Teams.Domain.Games.Entities;

public sealed class Game : AggregateRoot<GameId>
{
    public string Name { get; private set; }

    public Game(string name) : base(GameId.Create())
    {
        Name = name;
    }
}