namespace GatewayApi.Common.Models.Auth;

public sealed record RefreshSuccess(
    string AccessToken,
    long ExpiresIn);