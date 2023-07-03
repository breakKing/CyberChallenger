namespace Identity.Endpoints.Login;

public sealed record LoginResponse(
    string UserId,
    string AccessToken,
    long ExpiresIn);