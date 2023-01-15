using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Persistence.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Shared.Grpc.Interfaces;

namespace IdentityProviderService.Features.Tokens.RefreshTokens;

public sealed class RefreshTokensCommandHandler : ICommandHandler<RefreshTokensCommand, RefreshTokensResponse>
{
    private readonly ISessionManager _sessionManager;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;

    public RefreshTokensCommandHandler(ISessionManager sessionManager, UserManager<User> userManager, 
        ITokenService tokenService, ICurrentUserService currentUserService)
    {
        _sessionManager = sessionManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _currentUserService = currentUserService;
    }

    /// <inheritdoc />
    public async ValueTask<RefreshTokensResponse> Handle(RefreshTokensCommand command, CancellationToken ct)
    {
        var userId = _currentUserService.GetIdFromHttpContext();
        var userIdFromToken = _tokenService.GetUserIdFromToken(command.AccessToken);

        if (userId is null || userId != userIdFromToken)
        {
            return new RefreshTokensResponse(false, string.Empty, string.Empty);
        }
        
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());

        if (user is null)
        {
            return new RefreshTokensResponse(false, string.Empty, string.Empty);
        }

        var result = await _sessionManager.RenewSessionAsync(user, command.UserAgentFingerprint, 
            command.RefreshToken, ct);

        return result.Success ? 
            new RefreshTokensResponse(true, result.AccessToken, result.RefreshToken) : 
            new RefreshTokensResponse(false, string.Empty, string.Empty);
    }
}