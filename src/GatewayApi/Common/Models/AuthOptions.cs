namespace GatewayApi.Common.Models;

public sealed class AuthOptions
{
    public const string SectionName = "Auth";
    
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}