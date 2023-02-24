using GatewayApi.Common.Constants;
using GatewayApi.Common.Models.Auth;
using GatewayApi.Common.Models.Options;
using GatewayApi.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using OneOf;
using OpenIddict.Abstractions;

namespace GatewayApi.Services.Implementations;

public sealed class OpenIdClient : IOpenIdClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<AuthOptions> _authOptions;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ILogger<OpenIdClient> _logger;

    public OpenIdClient(IHttpClientFactory httpClientFactory, IOptions<AuthOptions> authOptions, 
        IRefreshTokenService refreshTokenService, ILogger<OpenIdClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _authOptions = authOptions;
        _refreshTokenService = refreshTokenService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<OneOf<LoginSuccess, LoginFail>> LoginAsync(string login, string password, 
        CancellationToken ct = default)
    {
        var openIdClient = _httpClientFactory.CreateClient(HttpClientNames.IdentityProviderService);

        var openIdRequest = new PasswordTokenRequest()
        {
            Address = OpenIdRoutes.Token,
            Method = HttpMethod.Post,
            GrantType = "password",
            ClientId = _authOptions.Value.ClientId,
            ClientSecret = _authOptions.Value.ClientSecret,
            UserName = login,
            Password = password
        };

        var openIdResponse = await openIdClient.RequestPasswordTokenAsync(openIdRequest, ct);

        if (openIdResponse is null)
        {
            _logger.LogError("Open id response was null when requesting token with {@Request}", openIdRequest);
            return new LoginFail("Unexpected error has occurred during login attempt");
        }

        if (openIdResponse.IsError)
        {
            return new LoginFail(openIdResponse.ErrorDescription);
        }

        var accessToken = openIdResponse.AccessToken!;
        var refreshToken = openIdResponse.RefreshToken!;
        var expiresInMinutes = openIdResponse.ExpiresIn / 60;

        var userInfoRequest = new UserInfoRequest
        {
            Address = OpenIdRoutes.UserInfo,
            Method = HttpMethod.Get,
            ClientId = _authOptions.Value.ClientId,
            ClientSecret = _authOptions.Value.ClientSecret,
            Token = accessToken
        };

        var userInfoResponse = await openIdClient.GetUserInfoAsync(userInfoRequest, ct);
        
        if (userInfoResponse is null)
        {
            _logger.LogError("Open id response was null when requesting user info with {@Request}", userInfoRequest);
            return new LoginFail("Unexpected error has occurred during login attempt");
        }
        
        if (userInfoResponse.IsError)
        {
            return new LoginFail(openIdResponse.ErrorDescription);
        }

        var userId = userInfoResponse.Claims
            .FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Subject)
            ?.Value!;

        await _refreshTokenService.StoreRefreshTokenAsync(refreshToken, accessToken, ct);

        return new LoginSuccess(userId, accessToken, refreshToken, expiresInMinutes);
    }
}