using GatewayApi.Common.Models.Auth;
using GatewayApi.Common.Models.Base;
using OneOf;

namespace GatewayApi.Services.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Попытка логина пользователя и получения токенов
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<OneOf<LoginSuccess, OperationFail>> LoginAsync(string login, string password, CancellationToken ct = default);
    
    /// <summary>
    /// Попытка обновления токенов пользователя
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<OneOf<RefreshSuccess, OperationFail>> RefreshAsync(string accessToken, CancellationToken ct = default);

    /// <summary>
    /// Отзыв access и рефреш токенов пользователя
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<OneOf<LogoutSuccess, OperationFail>> LogoutAsync(string accessToken, CancellationToken ct = default);
}