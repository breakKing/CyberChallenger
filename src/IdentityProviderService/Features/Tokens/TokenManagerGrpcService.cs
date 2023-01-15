using Grpc.Core;
using Shared.Contracts.IdentityProviderService;

namespace IdentityProviderService.Features.Tokens;

public sealed class TokenManagerGrpcService : TokenManager.TokenManagerBase
{
    /// <inheritdoc />
    public override async Task<ValidateAccessTokenGrpcResponse> ValidateAccessToken(
        ValidateAccessTokenGrpcRequest request, ServerCallContext context)
    {
        return await base.ValidateAccessToken(request, context);
    }

    /// <inheritdoc />
    public override async Task<RefreshTokensGrpcResponse> RefreshTokens(RefreshTokensGrpcRequest request,
        ServerCallContext context)
    {
        return await base.RefreshTokens(request, context);
    }
}