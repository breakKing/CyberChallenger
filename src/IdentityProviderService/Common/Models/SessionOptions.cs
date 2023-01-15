namespace IdentityProviderService.Common.Models;

public sealed class SessionOptions
{
    public const string SectionName = "UserSession";
    
    public int MaxConcurrentSessions { get; set; }
    public long SessionLifetimeInMinutes { get; set; }
}