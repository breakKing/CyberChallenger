using Grpc.Core;
using IdentityProviderService.Features.Tokens.RefreshTokens;
using IdentityProviderService.Features.Tokens.ValidateAccessToken;
using MapsterMapper;
using Mediator;
using Shared.Contracts.IdentityProviderService;

namespace IdentityProviderService.Features.Tokens;

public sealed class TokenManagerGrpcService : TokenManager.TokenManagerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public TokenManagerGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<ValidateAccessTokenGrpcResponse> ValidateAccessToken(
        ValidateAccessTokenGrpcRequest request, ServerCallContext context)
    {
        var mediatorRequest = _mapper.Map<ValidateAccessTokenCommand>(request);
        var mediatorResponse = await _mediator.Send(mediatorRequest, context.CancellationToken);
        var response = _mapper.Map<ValidateAccessTokenGrpcResponse>(mediatorResponse);
        
        return response;
    }

    /// <inheritdoc />
    public override async Task<RefreshTokensGrpcResponse> RefreshTokens(RefreshTokensGrpcRequest request,
        ServerCallContext context)
    {
        var mediatorRequest = _mapper.Map<RefreshTokensCommand>(request);
        var mediatorResponse = await _mediator.Send(mediatorRequest, context.CancellationToken);
        var response = _mapper.Map<RefreshTokensGrpcResponse>(mediatorResponse);
        
        return response;
    }
}