namespace IdentityProviderService.Features.Tokens.ValidateAccessToken;

public sealed record ValidateAccessTokenResponse(bool Success, string? UserId = null);
