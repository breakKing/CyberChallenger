using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Entities;

public sealed class Team : AggregateRoot
{
    private readonly List<Participant> _participants = new();
    
    public string Name { get; private set; }
    public Game Game { get; private set; }
    public IReadOnlyList<Participant> Participants => _participants.AsReadOnly();
    
    public Team(string name, Game game) : base(NewId.NextGuid())
    {
        Name = name;
        Game = game;
    }
}