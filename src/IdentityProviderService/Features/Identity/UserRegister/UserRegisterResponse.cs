namespace IdentityProviderService.Features.Identity.UserRegister;

public sealed record UserRegisterResponse(bool Success, string AccessToken, string RefreshToken);