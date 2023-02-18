using System.Security.Claims;
using IdentityProviderService.Common.Constants;
using IdentityProviderService.Features.Connect.Login;
using IdentityProviderService.Features.Connect.Refresh;
using IdentityProviderService.Persistence.Entities;
using Mediator;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace IdentityProviderService.Features.Connect;

[ApiController]
public sealed class ConnectController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public ConnectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(OpenIdRoutes.Token)]
    public async Task<IActionResult> Token(CancellationToken ct = default)
    {
        var oidcRequest = HttpContext.GetOpenIddictServerRequest();

        if (oidcRequest is null)
        {
            return BadRequest(new OpenIddictResponse
            {
                Error = OpenIddictConstants.Errors.InvalidRequest,
                ErrorDescription = "Request is not in the OAuth 2.0 form"
            });
        }

        var oidcResult = new OidcResult();

        if (oidcRequest.IsPasswordGrantType())
        {
            var loginCommand = new LoginCommand(
                oidcRequest.Username ?? string.Empty,
                oidcRequest.Password ?? string.Empty);

            var loginResponse = await _mediator.Send(loginCommand, ct);
            
            oidcResult = loginResponse.Result;
        }

        if (oidcRequest.IsRefreshTokenGrantType())
        {
            var info = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            
            var refreshCommand = new RefreshCommand(info.Principal!);

            var refreshResponse = await _mediator.Send(refreshCommand, ct);
            
            oidcResult = refreshResponse.Result;
        }

        if (!oidcResult.Succeeded)
        {
            return BadRequest(new OpenIddictResponse
            {
                Error = oidcResult.Error,
                ErrorDescription = oidcResult.ErrorDescription
            });
        }

        return SignIn(oidcResult.Principal!, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost(OpenIdRoutes.Authorize)]
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
    [HttpGet(OpenIdRoutes.UserInfo)]
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
    [HttpPost(OpenIdRoutes.Logout)]
    public async Task<IActionResult> Logout()
    {
        // TODO реализовать нормально
        throw new NotImplementedException();
    }
}