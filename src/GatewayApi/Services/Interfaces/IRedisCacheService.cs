using Microsoft.Extensions.Caching.Distributed;

namespace GatewayApi.Services.Interfaces;

public interface IRedisCacheService
{
    /// <summary>
    /// Получение записи из кеша по ключу
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ct"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T?> GetByKeyAsync<T>(string key, CancellationToken ct = default);

    /// <summary>
    /// Добавление записи в кеш
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <param name="ct"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task AddAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken ct = default);

    /// <summary>
    /// Удаление записи из кеша
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task RemoveAsync(string key, CancellationToken ct = default);
}