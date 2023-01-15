namespace IdentityProviderService.Persistence.Entities;

public sealed class Session
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public string Fingerprint { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public long ExpiresInMinutes { get; set; }
    
    public User? User { get; set; }

    public DateTimeOffset ExpiresAt => CreatedAt.AddMinutes(ExpiresInMinutes);

    public bool Expired => DateTimeOffset.UtcNow >= ExpiresAt;
}