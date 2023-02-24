namespace Shared.Contracts.GatewayApi.Auth.Refresh;

public sealed record RefreshResponse(
    string AccessToken,
    long ExpiresIn);