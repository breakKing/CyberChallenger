using Grpc.Core;
using MapsterMapper;
using Mediator;
using Shared.Contracts.TeamService;
using TeamService.Features.Crud.Create;

namespace TeamService.Features.Crud;

public sealed class TeamCrudGrpcService : TeamCrud.TeamCrudBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public TeamCrudGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<CreateTeamGrpcResponse> CreateTeam(CreateTeamGrpcRequest request, ServerCallContext context)
    {
        var mediatorRequest = _mapper.Map<CreateTeamCommand>(request);
        var mediatorResponse = await _mediator.Send(mediatorRequest, context.CancellationToken);
        var response = _mapper.Map<CreateTeamGrpcResponse>(mediatorResponse);

        return response;
    }
}