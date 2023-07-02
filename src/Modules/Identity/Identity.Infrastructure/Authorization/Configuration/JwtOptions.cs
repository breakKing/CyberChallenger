namespace Identity.Infrastructure.Authorization.Configuration;

public sealed class JwtOptions
{
    public string IssuerSigningPublicKeyFile { get; set; } = string.Empty;
    public string IssuerSigningPrivateKeyFile { get; set; } = string.Empty;
    public string IssuerEncryptionPrivateKeyFile { get; set; } = string.Empty;
    public string IssuerEncryptionPublicKeyFile { get; set; } = string.Empty;
    public string ValidAudience { get; set; } = string.Empty;
    public string ValidIssuer { get; set; } = string.Empty;
    public long AccessTokenExpirationTimeInMinutes { get; set; }
    public long RefreshTokenExpirationTimeInMinutes { get; set; }
    public bool ValidateLifetime { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool RequireAudience { get; set; }
    public bool RequireExpirationTime { get; set; }
    public bool RequireSignedTokens { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
}