using System.Collections.Immutable;
using System.Security.Claims;
using IdentityProvider.Common.Models;
using IdentityProvider.Features.Connect.UserInfo;
using IdentityProvider.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

namespace IdentityProvider.Features.Connect;

public sealed class ClaimsPrincipalService : IClaimsPrincipalService
{
    private readonly UserManager<User> _userManager;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public ClaimsPrincipalService(UserManager<User> userManager, IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions;
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

        identity.SetClaim(OpenIddictConstants.Claims.Subject, id);
        identity.SetClaim(OpenIddictConstants.Claims.Name, userName);
        identity.SetClaims(OpenIddictConstants.Claims.Role, roles.ToImmutableArray());
        identity.SetClaim(OpenIddictConstants.Claims.Audience, _jwtOptions.Value.ValidAudience);

        identity.SetDestinations(GetDestinations);

        var claimsPrincipal = new ClaimsPrincipal(identity);
        claimsPrincipal.SetScopes(
            OpenIddictConstants.Scopes.Roles, 
            OpenIddictConstants.Scopes.OfflineAccess,
            OpenIddictConstants.Scopes.Profile);

        return claimsPrincipal;
    }

    /// <inheritdoc />
    public UserInfoDto GetUserInfoFromPrincipal(ClaimsPrincipal principal)
    {
        var id = principal.GetClaim(OpenIddictConstants.Claims.Subject) ?? string.Empty;
        var userName = principal.GetClaim(OpenIddictConstants.Claims.Name) ?? string.Empty;
        var roles = principal.GetClaims(OpenIddictConstants.Claims.Role).ToList();
        
        return new UserInfoDto(id, userName, roles);
    }
    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        switch (claim.Type)
        {
            case OpenIddictConstants.Claims.Name:
                yield return OpenIddictConstants.Destinations.AccessToken;
                yield return OpenIddictConstants.Destinations.IdentityToken;
                yield break;

            case OpenIddictConstants.Claims.Role:
                yield return OpenIddictConstants.Destinations.AccessToken;
                yield return OpenIddictConstants.Destinations.IdentityToken;
                yield break;
            
            case "AspNet.Identity.SecurityStamp": 
                yield break;

            default:
                yield return OpenIddictConstants.Destinations.AccessToken;
                yield break;
        }
    }
}