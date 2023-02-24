using GatewayApi.Common.Models.Auth;
using OneOf;

namespace GatewayApi.Services.Interfaces;

public interface IOpenIdClient
{
    Task<OneOf<LoginSuccess, LoginFail>> LoginAsync(string login, string password, CancellationToken ct = default);
}