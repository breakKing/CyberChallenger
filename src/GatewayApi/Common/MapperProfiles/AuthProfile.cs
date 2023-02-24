using GatewayApi.Common.Models.Auth;
using GatewayApi.Common.Models.Base;
using Mapster;
using Shared.Contracts.GatewayApi.Auth.Login;
using Shared.Contracts.GatewayApi.Auth.Logout;
using Shared.Contracts.GatewayApi.Auth.Refresh;

namespace GatewayApi.Common.MapperProfiles;

public sealed class AuthProfile : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OperationFail, List<string>>()
            .Map(dest => dest, src => new List<string> { src.Error });
        
        config.NewConfig<LoginSuccess, LoginResponse>();
        
        config.NewConfig<RefreshSuccess, RefreshResponse>();
        
        config.NewConfig<LogoutSuccess, LogoutResponse>()
            .Map(dest => dest.Success, _ => true);
    }
}