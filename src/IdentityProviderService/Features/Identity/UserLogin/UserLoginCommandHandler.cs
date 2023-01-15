using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Persistence.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderService.Features.Identity.UserLogin;

public sealed class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, UserLoginResponse>
{
    private readonly SignInManager<User> _signInManager;
    private readonly ISessionManager _sessionManager;

    public UserLoginCommandHandler(SignInManager<User> signInManager, ISessionManager sessionManager)
    {
        _signInManager = signInManager;
        _sessionManager = sessionManager;
    }

    /// <inheritdoc />
    public async ValueTask<UserLoginResponse> Handle(UserLoginCommand command, CancellationToken ct)
    {
        var user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(
            u => u.UserName == command.UsernameOrEmail || u.Email == command.UsernameOrEmail, ct);

        if (user is null)
        {
            return new UserLoginResponse(false, string.Empty, string.Empty);
        }

        if (await _sessionManager.SessionExistsAsync(user.Id, command.UserAgentFingerprint, ct))
        {
            return new UserLoginResponse(false, string.Empty, string.Empty);
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);

        if (!signInResult.Succeeded)
        {
            return new UserLoginResponse(false, string.Empty, string.Empty);
        }

        var sessionResult = await _sessionManager.CreateSessionAsync(user, command.UserAgentFingerprint, ct);

        if (!sessionResult.Success)
        {
            return new UserLoginResponse(false, string.Empty, string.Empty);
        }
        
        return new UserLoginResponse(true, sessionResult.AccessToken, sessionResult.RefreshToken);
    }
}