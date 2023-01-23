using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace IdentityProviderService.Features.Connect;

[ApiController]
[Route("connect")]
public sealed class ConnectController : ControllerBase
{
    [HttpPost("token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        if (!result.Succeeded || result.Principal is null)
        {
            throw new InvalidOperationException("The OpenID Connect request cannot be handled due to authentication failure.");
        }

        var claimsPrincipal = result.Principal;

        if (request.IsPasswordGrantType())
        {
            // TODO логин по паролю
        }

        else if (request.IsRefreshTokenGrantType())
        {
            // TODO выдача refresh token'а
        }

        else
        {
            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost("authorize")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        // TODO реализовать нормально
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        var claims = new List<Claim>();
        var identity = new ClaimsIdentity(claims, "OpenIddict");
        var principal = new ClaimsPrincipal(identity);

        // Create a new authentication ticket holding the user identity.
        var ticket = new AuthenticationTicket(principal,
            new AuthenticationProperties(), 
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
        return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
    }
    
    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("userinfo")]
    public async Task<IActionResult> Userinfo()
    {
        // TODO реализовать нормально
        var claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal!;

        return Ok(new
        {
            Name = claimsPrincipal.GetClaim(OpenIddictConstants.Claims.Subject),
            Occupation = "Developer",
            Age = 43
        });
    }

    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        // TODO реализовать нормально
        throw new NotImplementedException();
    }
}