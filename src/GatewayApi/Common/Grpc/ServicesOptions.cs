namespace GatewayApi.Common.Grpc;

public sealed class ServicesOptions
{
    public const string SectionName = "Services";

    public string GameService { get; set; } = string.Empty;
    public string IdentityProviderService { get; set; } = string.Empty;
    public string TeamService { get; set; } = string.Empty;
    public string TournamentService { get; set; } = string.Empty;
    public string UserProfileService { get; set; } = string.Empty;
}