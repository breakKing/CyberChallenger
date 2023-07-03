using IdentityProvider.Persistence.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;

namespace IdentityProvider.Features.Connect.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IClaimsPrincipalService _claimsPrincipalService;
    
    private static readonly OidcResult InvalidLoginResult = 
        new OidcResult(OpenIddictConstants.Errors.AccessDenied, "Login credentials are invalid");
    
    public LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, 
        IClaimsPrincipalService claimsPrincipalService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _claimsPrincipalService = claimsPrincipalService;
    }

    /// <inheritdoc />
    public async ValueTask<LoginResponse> Handle(LoginCommand command, CancellationToken ct)
    {
        var login = command.Login;
        var password = command.Password;

        var user = await FindUserByLoginAsync(login);

        if (user is null)
        {
            return new LoginResponse(InvalidLoginResult);
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!signInResult.Succeeded)
        {
            return new LoginResponse(InvalidLoginResult);
        }

        var claimsPrincipal = await _claimsPrincipalService.CreateClaimsPrincipalAsync(user, ct);

        var result = new OidcResult
        {
            Succeeded = true,
            Principal = claimsPrincipal
        };

        return new LoginResponse(result);
    }

    private async Task<User?> FindUserByLoginAsync(string login)
    {
        var user = await _userManager.FindByNameAsync(login);

        if (user is not null)
        {
            return user;
        }

        user = await _userManager.FindByEmailAsync(login);

        return user;
    }
}