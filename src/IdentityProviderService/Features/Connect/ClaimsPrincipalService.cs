using System.Security.Claims;
using IdentityProviderService.Features.Connect.UserInfo;
using IdentityProviderService.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

namespace IdentityProviderService.Features.Connect;

public sealed class ClaimsPrincipalService : IClaimsPrincipalService
{
    private readonly UserManager<User> _userManager;

    public ClaimsPrincipalService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(User user, CancellationToken ct = default)
    {
        var id = user.Id.ToString();
        var userName = user.UserName ?? string.Empty;
        var roles = await _userManager.GetRolesAsync(user);
        
        var identity = new ClaimsIdentity(
            TokenValidationParameters.DefaultAuthenticationType,
            OpenIddictConstants.Claims.Name,
            OpenIddictConstants.Claims.Role);

        identity.AddClaim(OpenIddictConstants.Claims.Subject, id, OpenIddictConstants.Destinations.AccessToken);
        identity.AddClaim(OpenIddictConstants.Claims.Username, userName, OpenIddictConstants.Destinations.AccessToken);

        foreach (var userRole in roles)
        {
            identity.AddClaim(OpenIddictConstants.Claims.Role, userRole, OpenIddictConstants.Destinations.AccessToken);
        }

        var claimsPrincipal = new ClaimsPrincipal(identity);

        return claimsPrincipal;
    }

    /// <inheritdoc />
    public UserInfoDto GetUserInfoFromPrincipal(ClaimsPrincipal principal)
    {
        var id = principal.GetClaim(OpenIddictConstants.Claims.Subject) ?? string.Empty;
        var userName = principal.GetClaim(OpenIddictConstants.Claims.Username) ?? string.Empty;
        var roles = principal.GetClaims(OpenIddictConstants.Claims.Role).ToList();
        
        return new UserInfoDto(id, userName, roles);
    }
}