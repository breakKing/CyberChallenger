using IdentityProviderService.Persistence.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;

namespace IdentityProviderService.Features.Connect.Refresh;

public sealed class RefreshCommandHandler : ICommandHandler<RefreshCommand, RefreshResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IClaimsPrincipalService _claimsPrincipalService;
    
    private static readonly OidcResult InvalidRefreshTokenResult = 
        new OidcResult(OpenIddictConstants.Errors.InvalidGrant, "The refresh token is no longer valid");
    
    private static readonly OidcResult InvalidSignInResult = 
        new OidcResult(OpenIddictConstants.Errors.InvalidGrant, "The user is no longer allowed to sign in");
    
    public RefreshCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, 
        IClaimsPrincipalService claimsPrincipalService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _claimsPrincipalService = claimsPrincipalService;
    }

    /// <inheritdoc />
    public async ValueTask<RefreshResponse> Handle(RefreshCommand command, CancellationToken ct)
    {
        var principal = command.Principal;
        var user = await _userManager.GetUserAsync(principal);
        
        if (user is  null)
        {
            return new RefreshResponse(InvalidRefreshTokenResult);
        }
        
        if (!await _signInManager.CanSignInAsync(user))
        {
            return new RefreshResponse(InvalidSignInResult);;
        }

        var claimsPrincipal = await _claimsPrincipalService.CreateClaimsPrincipalAsync(user, ct);

        var result = new OidcResult
        {
            Succeeded = true,
            Principal = claimsPrincipal
        };

        return new RefreshResponse(result);
    }
}