namespace IdentityProviderService.Common.Models;

public sealed record SessionOperationResult(bool Success, string AccessToken, string RefreshToken);