namespace Shared.Contracts.GatewayApi.Auth.Login;

public sealed class LoginRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}