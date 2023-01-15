namespace IdentityProviderService.Features.Identity.UserLogin;

public sealed record UserLoginResponse(bool Success, string AccessToken, string RefreshToken);