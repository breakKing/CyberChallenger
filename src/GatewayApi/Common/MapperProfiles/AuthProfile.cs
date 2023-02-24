using GatewayApi.Common.Models.Auth;
using Mapster;
using Shared.Contracts.GatewayApi.Auth.Login;

namespace GatewayApi.Common.MapperProfiles;

public sealed class AuthProfile : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginSuccess, LoginResponse>();
        
        config.NewConfig<LoginFail, List<string>>()
            .Map(dest => dest, src => new List<string> { src.Error });
    }
}