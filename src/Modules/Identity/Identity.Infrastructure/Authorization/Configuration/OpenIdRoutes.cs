namespace Identity.Infrastructure.Authorization.Configuration;

public sealed class OpenIdRoutes
{
    public string Token { get; set; } = null!;
    public string Revocation { get; set; } = null!;
    public string UserInfo { get; set; } = null!;
    public string Introspection { get; set; } = null!;
}