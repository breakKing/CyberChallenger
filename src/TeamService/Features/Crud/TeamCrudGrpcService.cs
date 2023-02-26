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
    public override async Task<ReadManyTeamsByOffsetGrpcResponse> ReadManyTeamsByOffset(
        ReadManyTeamsByOffsetGrpcRequest request, ServerCallContext context)
    {
        return await base.ReadManyTeamsByOffset(request, context);
    }

    /// <inheritdoc />
    public override async Task<ReadManyTeamsByCursorGrpcResponse> ReadManyTeamsByCursor(
        ReadManyTeamsByCursorGrpcRequest request, ServerCallContext context)
    {
        return await base.ReadManyTeamsByCursor(request, context);
    }

    /// <inheritdoc />
    public override async Task<ReadOneTeamByIdGrpcResponse> ReadOneTeamById(ReadOneTeamByIdGrpcRequest request, 
        ServerCallContext context)
    {
        return await base.ReadOneTeamById(request, context);
    }
    
    /// <inheritdoc />
    public override async Task<CreateTeamGrpcResponse> CreateTeam(CreateTeamGrpcRequest request, 
        ServerCallContext context)
    {
        var mediatorRequest = _mapper.Map<CreateTeamCommand>(request);
        var mediatorResponse = await _mediator.Send(mediatorRequest, context.CancellationToken);
        var response = _mapper.Map<CreateTeamGrpcResponse>(mediatorResponse);

        return response;
    }

    /// <inheritdoc />
    public override async Task<UpdateTeamGrpcResponse> UpdateTeam(UpdateTeamGrpcRequest request, 
        ServerCallContext context)
    {
        return await base.UpdateTeam(request, context);
    }

    /// <inheritdoc />
    public override async Task<DeleteTeamGrpcResponse> DeleteTeam(DeleteTeamGrpcRequest request, 
        ServerCallContext context)
    {
        return await base.DeleteTeam(request, context);
    }
}