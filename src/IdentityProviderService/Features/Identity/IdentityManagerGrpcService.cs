using Grpc.Core;
using IdentityProviderService.Features.Identity.UserLogin;
using IdentityProviderService.Features.Identity.UserLogout;
using IdentityProviderService.Features.Identity.UserRegister;
using MapsterMapper;
using Mediator;
using Shared.Contracts.IdentityProviderService;

namespace IdentityProviderService.Features.Identity;

public sealed class IdentityManagerGrpcService : IdentityManager.IdentityManagerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public IdentityManagerGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<UserRegisterGrpcResponse> UserRegister(UserRegisterGrpcRequest request,
        ServerCallContext context)
    {
        var mediatorRequest = _mapper.Map<UserRegisterCommand>(request);
        var mediatorResponse = await _mediator.Send(mediatorRequest, context.CancellationToken);
        var response = _mapper.Map<UserRegisterGrpcResponse>(mediatorResponse);
        
        return response;
    }

    /// <inheritdoc />
    public override async Task<UserLoginGrpcResponse> UserLogin(UserLoginGrpcRequest request, ServerCallContext context)
    {
        var mediatorRequest = _mapper.Map<UserLoginCommand>(request);
        var mediatorResponse = await _mediator.Send(mediatorRequest, context.CancellationToken);
        var response = _mapper.Map<UserLoginGrpcResponse>(mediatorResponse);
        
        return response;
    }

    /// <inheritdoc />
    public override async Task<UserLogoutGrpcResponse> UserLogout(UserLogoutGrpcRequest request,
        ServerCallContext context)
    {
        var mediatorRequest = _mapper.Map<UserLogoutCommand>(request);
        var mediatorResponse = await _mediator.Send(mediatorRequest, context.CancellationToken);
        var response = _mapper.Map<UserLogoutGrpcResponse>(mediatorResponse);
        
        return response;
    }
}