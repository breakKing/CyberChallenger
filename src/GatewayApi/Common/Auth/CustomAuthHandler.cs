using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Shared.Contracts.Common;
using Shared.Contracts.IdentityProviderService;

namespace GatewayApi.Common.Auth;

public sealed class CustomAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly TokenManager.TokenManagerClient _client;
    
    /// <inheritdoc />
    public CustomAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, TokenManager.TokenManagerClient client) : 
        base(options, logger, encoder, clock)
    {
        _client = client;
    }

    /// <inheritdoc />
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(AuthConstants.JwtTokenHeader))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
        
        string accessToken = Request.Headers[AuthConstants.JwtTokenHeader]!;
        
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return AuthenticateResult.NoResult();
        }
        
        if (!Request.Headers.ContainsKey(AuthConstants.UserAgentFingerprintHeader))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        string fingerprint = Request.Headers[AuthConstants.UserAgentFingerprintHeader]!;
 
        if (string.IsNullOrWhiteSpace(fingerprint))
        {
            return AuthenticateResult.NoResult();
        }
 
        try
        {
            return await ValidateTokenAsync(accessToken, fingerprint);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }
    }
 
    private async Task<AuthenticateResult> ValidateTokenAsync(string accessToken, string userAgentFingerprint)
    {
        var grpcRequest = new ValidateAccessTokenGrpcRequest
        {
            AccessToken = accessToken,
            UserAgentFingerprint = userAgentFingerprint
        };

        var grpcResponse = await _client.ValidateAccessTokenAsync(grpcRequest);
        
        if (!grpcResponse.Success)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
        
        var claims = new List<Claim>
        {
            new(GlobalConstants.UserIdInternalHeader, grpcResponse.UserId)
        };
 
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new System.Security.Principal.GenericPrincipal(identity, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        
        return AuthenticateResult.Success(ticket);
    }
}