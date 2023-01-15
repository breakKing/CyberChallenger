using Mediator;

namespace IdentityProviderService.Features.Tokens.ValidateAccessToken;

public sealed record ValidateAccessTokenCommand(string AccessToken, string UserAgentFingerprint) : 
    ICommand<ValidateAccessTokenResponse>;