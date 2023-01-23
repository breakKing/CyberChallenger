using IdentityProviderService.Common.Interfaces;
using Mediator;
using Shared.Grpc.Interfaces;

namespace IdentityProviderService.Features.Tokens.ValidateAccessToken;

public class ValidateAccessTokenCommandHandler : ICommandHandler<ValidateAccessTokenCommand, ValidateAccessTokenResponse>
{
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISessionManager _sessionManager;

    public ValidateAccessTokenCommandHandler(ITokenService tokenService, ICurrentUserService currentUserService, 
        ISessionManager sessionManager)
    {
        _tokenService = tokenService;
        _currentUserService = currentUserService;
        _sessionManager = sessionManager;
    }

    /// <inheritdoc />
    public async ValueTask<ValidateAccessTokenResponse> Handle(ValidateAccessTokenCommand command, CancellationToken ct)
    {
        if (!_tokenService.ValidateToken(command.AccessToken))
        {
            return new ValidateAccessTokenResponse(false);
        }

        var userId = _tokenService.GetUserIdFromToken(command.AccessToken);

        if (userId is null)
        {
            return new ValidateAccessTokenResponse(false);
        }

        var sessionExists = await _sessionManager.SessionExistsAsync(userId.Value, command.UserAgentFingerprint, ct);

        if (!sessionExists)
        {
            return new ValidateAccessTokenResponse(false);
        }
        
        return new ValidateAccessTokenResponse(true, userId.ToString());
    }
}