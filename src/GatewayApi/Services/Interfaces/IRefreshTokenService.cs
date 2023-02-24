namespace GatewayApi.Services.Interfaces;

public interface IRefreshTokenService
{
    /// <summary>
    /// Получение рефреш-токена для заданного access-токена
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<string?> GetRefreshTokenByAccessTokenAsync(string accessToken, CancellationToken ct = default);

    /// <summary>
    /// Сохранение связки рефреш-токена с соответствующим access-токеном
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="accessToken"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task StoreRefreshTokenAsync(string refreshToken, string accessToken, CancellationToken ct = default);
    
    /// <summary>
    /// Удаление связки рефреш-токена с соответствующим access-токеном
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="accessToken"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task RemoveRefreshTokenAsync(string refreshToken, string accessToken, CancellationToken ct = default);
}