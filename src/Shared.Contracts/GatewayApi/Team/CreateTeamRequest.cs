namespace Shared.Contracts.GatewayApi.Team;

public sealed class CreateTeamRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid GameId { get; set; } = Guid.Empty;
}