namespace Identity.Common.Configuration;

public sealed class AuthOptions
{
    public const string SectionName = "Auth";
    
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int RefreshTokenStoreTimeInMinutes { get; set; }
    public string IdentityProviderAddress { get; set; } = null!;
}