using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Persistence.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Shared.Grpc.Interfaces;

namespace IdentityProviderService.Features.Identity.UserLogout;

public sealed class UserLogoutCommandHandler : ICommandHandler<UserLogoutCommand, UserLogoutResponse>
{
    private readonly ISessionManager _sessionManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<User> _userManager;

    public UserLogoutCommandHandler(ISessionManager sessionManager, ICurrentUserService currentUserService, 
        UserManager<User> userManager)
    {
        _sessionManager = sessionManager;
        _currentUserService = currentUserService;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async ValueTask<UserLogoutResponse> Handle(UserLogoutCommand command, CancellationToken ct)
    {
        var userId = _currentUserService.GetIdFromHttpContext();

        if (userId is null)
        {
            return new UserLogoutResponse();
        }

        var user = await _userManager.FindByIdAsync(userId.Value.ToString());

        if (user is null)
        {
            return new UserLogoutResponse();
        }

        await _sessionManager.DropSessionAsync(user, command.UserAgentFingerprint, ct);
        
        return new UserLogoutResponse();
    }
}