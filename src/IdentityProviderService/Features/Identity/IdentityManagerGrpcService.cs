using Grpc.Core;
using Shared.Contracts.IdentityProviderService;

namespace IdentityProviderService.Features.Identity;

public sealed class IdentityManagerGrpcService : IdentityManager.IdentityManagerBase
{
    /// <inheritdoc />
    public override async Task<UserRegisterGrpcResponse> UserRegister(UserRegisterGrpcRequest request,
        ServerCallContext context)
    {
        return await base.UserRegister(request, context);
    }

    /// <inheritdoc />
    public override async Task<UserLoginGrpcResponse> UserLogin(UserLoginGrpcRequest request, ServerCallContext context)
    {
        return await base.UserLogin(request, context);
    }

    /// <inheritdoc />
    public override async Task<UserLogoutGrpcResponse> UserLogout(UserLogoutGrpcRequest request,
        ServerCallContext context)
    {
        return await base.UserLogout(request, context);
    }
}