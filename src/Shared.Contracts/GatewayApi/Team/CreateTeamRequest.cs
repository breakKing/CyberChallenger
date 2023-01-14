namespace Shared.Contracts.GatewayApi.Team;

public sealed record CreateTeamRequest
{
    public string Name { get; init; }
    public Guid GameId { get; init; }
    
    public CreateTeamRequest(string name, Guid gameId)
    {
        Name = name;
        GameId = gameId;
    }
    
    public CreateTeamRequest() : this(string.Empty, Guid.Empty)
    {
        
    }

    public void Deconstruct(out string name, out Guid gameId)
    {
        name = Name;
        gameId = GameId;
    }
}