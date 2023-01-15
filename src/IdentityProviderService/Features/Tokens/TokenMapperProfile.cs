using IdentityProviderService.Features.Tokens.RefreshTokens;
using IdentityProviderService.Features.Tokens.ValidateAccessToken;
using Mapster;
using Shared.Contracts.IdentityProviderService;

namespace IdentityProviderService.Features.Tokens;

public sealed class TokenMapperProfile : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RefreshTokensGrpcRequest, RefreshTokensCommand>();
        config.NewConfig<RefreshTokensResponse, RefreshTokensGrpcResponse>();
        
        config.NewConfig<ValidateAccessTokenGrpcRequest, ValidateAccessTokenCommand>();
        config.NewConfig<ValidateAccessTokenResponse, ValidateAccessTokenGrpcResponse>();
    }
}