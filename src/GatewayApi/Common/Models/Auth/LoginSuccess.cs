namespace GatewayApi.Common.Models.Auth;

public sealed record LoginSuccess(
    string UserId,
    string AccessToken,
    string RefreshToken,
    long ExpiresIn);