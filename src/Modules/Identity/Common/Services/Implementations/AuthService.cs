using Identity.Common.Configuration;
using Identity.Common.Constants;
using Identity.Common.Models;
using Identity.Common.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneOf;

namespace Identity.Common.Services.Implementations;

public sealed class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<AuthOptions> _authOptions;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IHttpClientFactory httpClientFactory, 
        IOptions<AuthOptions> authOptions, 
        IRefreshTokenService refreshTokenService, 
        ILogger<AuthService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _authOptions = authOptions;
        _refreshTokenService = refreshTokenService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<OneOf<LoginSuccess, OperationFail>> LoginAsync(string login, string password, 
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

        TokenResponse? openIdResponse;
        try
        {
            openIdResponse = await openIdClient.RequestPasswordTokenAsync(openIdRequest, ct);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception was thrown while requesting for password grant: {@Message}", e.Message);
            return new OperationFail("Unexpected error has occurred during login attempt");
        }

        if (openIdResponse is null)
        {
            _logger.LogError("Open id response was null when requesting token for user {@User}", 
                openIdRequest.UserName);
            
            return new OperationFail("Unexpected error has occurred during login attempt");
        }

        if (openIdResponse.IsError)
        {
            return new OperationFail(openIdResponse.ErrorDescription);
        }

        var accessToken = openIdResponse.AccessToken!;
        var refreshToken = openIdResponse.RefreshToken!;
        var expiresIn = openIdResponse.ExpiresIn;
        
        openIdClient.SetBearerToken(accessToken);

        UserInfoResponse? userInfoResponse;
        try
        {
            userInfoResponse = await GetUserInfoAsync(openIdClient, accessToken, ct);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception was thrown while requesting for user info: {@Message}", e.Message);
            return new OperationFail("Unexpected error has occurred during login attempt");
        }

        if (userInfoResponse is null)
        {
            return new OperationFail("Unexpected error has occurred during login attempt");
        }
        
        if (userInfoResponse.IsError)
        {
            return new OperationFail(openIdResponse.ErrorDescription);
        }

        var userId = userInfoResponse.Claims.FirstOrDefault(c => c.Type == "id")?.Value!;

        await _refreshTokenService.StoreRefreshTokenAsync(refreshToken, accessToken, ct);

        return new LoginSuccess(userId, accessToken, refreshToken, expiresIn);
    }

    /// <inheritdoc />
    public async Task<OneOf<RefreshSuccess, OperationFail>> RefreshAsync(string accessToken, 
        CancellationToken ct = default)
    {
        var openIdClient = _httpClientFactory.CreateClient(HttpClientNames.IdentityProviderService);
        openIdClient.SetBearerToken(accessToken);

        var refreshToken = await _refreshTokenService.GetRefreshTokenByAccessTokenAsync(accessToken, ct);

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            _logger.LogWarning("Attempt to get a refresh token failed due to its absence in the cache");
            return new OperationFail("Failed to refresh access token");
        }
        
        var openIdRequest = new RefreshTokenRequest
        {
            Address = OpenIdRoutes.Token,
            Method = HttpMethod.Post,
            ClientId = _authOptions.Value.ClientId,
            ClientSecret = _authOptions.Value.ClientSecret,
            GrantType = "refresh_token",
            RefreshToken = refreshToken
        };
        
        TokenResponse? openIdResponse;
        try
        {
            openIdResponse = await openIdClient.RequestRefreshTokenAsync(openIdRequest, ct);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception was thrown while requesting for refresh token grant: {@Message}", e.Message);
            return new OperationFail("Unexpected error has occurred during refresh attempt");
        }
        
        if (openIdResponse is null)
        {
            _logger.LogError("Open id response was null when requesting refresh");
            return new OperationFail("Unexpected error has occurred during refresh attempt");
        }

        if (openIdResponse.IsError)
        {
            return new OperationFail(openIdResponse.ErrorDescription);
        }
        
        var newAccessToken = openIdResponse.AccessToken!;
        var newRefreshToken = openIdResponse.RefreshToken!;
        var expiresIn = openIdResponse.ExpiresIn;

        try
        {
            await _refreshTokenService.RemoveRefreshTokenAsync(refreshToken, accessToken, ct);
            await _refreshTokenService.StoreRefreshTokenAsync(newRefreshToken, newAccessToken, ct);
        }
        catch (Exception e)
        {
            await _refreshTokenService.StoreRefreshTokenAsync(refreshToken, accessToken, ct);
            _logger.LogError("Exception was thrown while updating refresh token in the cache: {@Message}", e.Message);
            return new OperationFail("Unexpected error has occurred during refresh attempt");
        }
        
        openIdClient.SetBearerToken(newAccessToken);

        await RevokeTokenAsync(openIdClient, accessToken, ct);
        await RevokeTokenAsync(openIdClient, refreshToken, ct);

        return new RefreshSuccess(newAccessToken, expiresIn);
    }

    /// <inheritdoc />
    public async Task<OneOf<LogoutSuccess, OperationFail>> LogoutAsync(string accessToken, CancellationToken ct = default)
    {
        var openIdClient = _httpClientFactory.CreateClient(HttpClientNames.IdentityProviderService);
        openIdClient.SetBearerToken(accessToken);
        
        var refreshToken = await _refreshTokenService.GetRefreshTokenByAccessTokenAsync(accessToken, ct);

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            _logger.LogWarning("Attempt to get a refresh token failed due to its absence in the cache");
            return new OperationFail("Failed to refresh access token");
        }

        try
        {
            await RevokeTokenAsync(openIdClient, refreshToken, ct);
            await RevokeTokenAsync(openIdClient, accessToken, ct);
            await _refreshTokenService.RemoveRefreshTokenAsync(refreshToken, accessToken, ct);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception was thrown while logout: {@Message}", e.Message);
            return new OperationFail("Unexpected error has occurred during logout");
        }

        return new LogoutSuccess();
    }

    private async Task<UserInfoResponse?> GetUserInfoAsync(HttpClient openIdClient, string accessToken, 
        CancellationToken ct = default)
    {
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
            _logger.LogError("Open id response was null when requesting user info");
        }

        return userInfoResponse;
    }

    private async Task RevokeTokenAsync(HttpClient openIdClient, string token, CancellationToken ct = default)
    {
        var revokeRequest = new TokenRevocationRequest()
        {
            Address = OpenIdRoutes.Revocation,
            Method = HttpMethod.Post,
            ClientId = _authOptions.Value.ClientId,
            ClientSecret = _authOptions.Value.ClientSecret,
            Token = token
        };

        try
        {
            await openIdClient.RevokeTokenAsync(revokeRequest, ct);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception was thrown while revoking token: {@Message}", e.Message);
        }
    }
}