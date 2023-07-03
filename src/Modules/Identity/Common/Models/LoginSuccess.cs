namespace Identity.Common.Models;

public sealed record LoginSuccess(
    string UserId,
    string AccessToken,
    string RefreshToken,
    long ExpiresIn);