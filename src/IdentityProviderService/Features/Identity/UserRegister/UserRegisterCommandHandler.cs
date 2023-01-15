using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Persistence.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderService.Features.Identity.UserRegister;

public sealed class UserRegisterCommandHandler : ICommandHandler<UserRegisterCommand, UserRegisterResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ISessionManager _sessionManager;

    public UserRegisterCommandHandler(UserManager<User> userManager, ISessionManager sessionManager)
    {
        _userManager = userManager;
        _sessionManager = sessionManager;
    }

    /// <inheritdoc />
    public async ValueTask<UserRegisterResponse> Handle(UserRegisterCommand command, CancellationToken ct)
    {
        var userExists = await _userManager.Users.AnyAsync(
            u => u.UserName == command.UserName || u.Email == command.Email, ct);

        if (userExists)
        {
            return new UserRegisterResponse(false, string.Empty, string.Empty);
        }

        if (command.Password != command.PasswordRepeat)
        {
            return new UserRegisterResponse(false, string.Empty, string.Empty);
        }

        var user = new User
        {
            UserName = command.UserName,
            Email = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            return new UserRegisterResponse(false, string.Empty, string.Empty);
        }

        var sessionResult = await _sessionManager.CreateSessionAsync(user, command.UserAgentFingerprint, ct);

        if (!sessionResult.Success)
        {
            return new UserRegisterResponse(true, string.Empty, string.Empty);
        }

        return new UserRegisterResponse(true, sessionResult.AccessToken, sessionResult.RefreshToken);
    }
}