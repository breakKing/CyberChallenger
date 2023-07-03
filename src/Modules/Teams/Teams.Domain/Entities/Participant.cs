using Common.Domain.Primitives;
using MassTransit;

namespace Teams.Domain.Entities;

public sealed class Participant : Entity
{
    private readonly List<Team> _teams = new();
    
    public string Nickname { get; private set; }

    public IReadOnlyList<Team> Teams => _teams.AsReadOnly();

    public Participant(string nickname) : base(NewId.NextGuid())
    {
        Nickname = nickname;
    }
}