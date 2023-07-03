namespace Identity.Common.Models;

public sealed record RefreshSuccess(
    string AccessToken,
    long ExpiresIn);