using System.Security.Cryptography;
using System.Text;
using GatewayApi.Common.Models.Options;
using GatewayApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace GatewayApi.Services.Implementations;

public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly IRedisCacheService _redisCache;
    private readonly IOptions<AuthOptions> _authOptions;

    public RefreshTokenService(IRedisCacheService redisCache, IOptions<AuthOptions> authOptions)
    {
        _redisCache = redisCache;
        _authOptions = authOptions;
    }

    /// <inheritdoc />
    public async Task<string?> GetRefreshTokenByAccessTokenAsync(string accessToken, CancellationToken ct = default)
    {
        var key = GenerateCacheKey(accessToken);
        
        return await _redisCache.GetByKeyAsync<string>(key, ct);
    }

    /// <inheritdoc />
    public async Task StoreRefreshTokenAsync(string refreshToken, string accessToken, CancellationToken ct = default)
    {
        var key = GenerateCacheKey(accessToken);
        
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_authOptions.Value.RefreshTokenStoreTimeInMinutes)
        };

        await _redisCache.AddAsync(key, refreshToken, options, ct);
    }

    /// <inheritdoc />
    public async Task RemoveRefreshTokenAsync(string refreshToken, string accessToken, CancellationToken ct = default)
    {
        var key = GenerateCacheKey(accessToken);

        await _redisCache.RemoveAsync(key, ct);
    }
    
    private static string GenerateCacheKey(string accessToken)
    {
        var bytes = Encoding.Default.GetBytes(accessToken);
        var hashBytes = SHA512.HashData(bytes);

        var hashString = Encoding.Default.GetString(hashBytes);

        return hashString;
    }
}