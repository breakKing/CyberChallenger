using System.Text.Json;
using GatewayApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace GatewayApi.Services.Implementations;

public sealed class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;
    
    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    /// <inheritdoc />
    public async Task<T?> GetByKeyAsync<T>(string key, CancellationToken ct = default)
    {
        var value = await _cache.GetStringAsync(key, ct);
 
        if (value is not null)
        {
            return JsonSerializer.Deserialize<T>(value);
        }
 
        return default;
    }

    /// <inheritdoc />
    public async Task AddAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken ct = default)
    {
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options, ct);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(string key, CancellationToken ct = default)
    {
        await _cache.RemoveAsync(key, ct);
    }
}