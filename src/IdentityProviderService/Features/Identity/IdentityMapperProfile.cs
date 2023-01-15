using IdentityProviderService.Features.Identity.UserLogin;
using IdentityProviderService.Features.Identity.UserLogout;
using IdentityProviderService.Features.Identity.UserRegister;
using Mapster;
using Shared.Contracts.IdentityProviderService;

namespace IdentityProviderService.Features.Identity;

public sealed class IdentityMapperProfile : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserLoginGrpcRequest, UserLoginCommand>();
        config.NewConfig<UserLoginResponse, UserLoginGrpcResponse>();
        
        config.NewConfig<UserLogoutGrpcRequest, UserLogoutCommand>();
        config.NewConfig<UserLogoutResponse, UserLogoutGrpcResponse>();
        
        config.NewConfig<UserRegisterGrpcRequest, UserRegisterCommand>();
        config.NewConfig<UserRegisterResponse, UserRegisterGrpcResponse>();
    }
}