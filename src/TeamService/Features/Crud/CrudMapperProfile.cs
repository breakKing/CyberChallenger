using Mapster;
using Shared.Contracts.TeamService;
using TeamService.Features.Crud.Create;

namespace TeamService.Features.Crud;

public sealed class CrudMapperProfile : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTeamGrpcRequest, CreateTeamCommand>();
        config.NewConfig<CreateTeamResponse, CreateTeamGrpcResponse>();
    }
}