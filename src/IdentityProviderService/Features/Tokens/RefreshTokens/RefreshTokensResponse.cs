namespace IdentityProviderService.Features.Tokens.RefreshTokens;

public sealed record RefreshTokensResponse(bool Success, string AccessToken, string RefreshToken);