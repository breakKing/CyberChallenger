namespace Shared.Contracts.GatewayApi.Auth.Login;

public sealed record LoginResponse(
    string UserId,
    string AccessToken,
    long ExpiresIn);