using System.Security.Claims;
using IdentityProviderService.Common.Constants;
using IdentityProviderService.Features.Connect.Login;
using IdentityProviderService.Features.Connect.Refresh;
using IdentityProviderService.Features.Connect.UserInfo;
using Mediator;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
            return BadOAuthRequest();
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

        else if (oidcRequest.IsRefreshTokenGrantType())
        {
            var info = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var refreshCommand = new RefreshCommand(info.Principal!);

            var refreshResponse = await _mediator.Send(refreshCommand, ct);

            oidcResult = refreshResponse.Result;
        }

        if (!oidcResult.Succeeded)
        {
            return ForbidFromOidcResult(oidcResult);
        }

        return SignIn(oidcResult.Principal!, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [Authorize]
    [HttpGet(OpenIdRoutes.UserInfo)]
    public async Task<IActionResult> Userinfo(CancellationToken ct = default)
    {
        var claimsPrincipal =
            (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal!;

        var userInfoQuery = new GetUserInfoQuery(claimsPrincipal);
        var userInfo = await _mediator.Send(userInfoQuery, ct);

        return Ok(userInfo);
    }

    [Authorize]
    [HttpPost(OpenIdRoutes.Logout)]
    public async Task<IActionResult> Logout()
    {
        return SignOut(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private IActionResult ForbidFromOidcResult(OidcResult result)
    {
        return Forbid(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties(new Dictionary<string, string?>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = result.Error,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = result.ErrorDescription
            }));
    }

    private IActionResult BadOAuthRequest()
    {
        return BadRequest(
            new OpenIddictResponse
            {
                Error = OpenIddictConstants.Errors.InvalidRequest,
                ErrorDescription = "Request is not in the OAuth 2.0 form"
            });
    }
}