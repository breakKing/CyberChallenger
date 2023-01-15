using Mediator;

namespace IdentityProviderService.Features.Tokens.RefreshTokens;

public sealed record RefreshTokensCommand(string AccessToken, string RefreshToken, string UserAgentFingerprint) : ICommand<RefreshTokensResponse>;