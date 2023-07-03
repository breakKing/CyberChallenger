namespace Identity.Endpoints.Refresh;

public sealed record RefreshResponse(
    string AccessToken,
    long ExpiresIn);