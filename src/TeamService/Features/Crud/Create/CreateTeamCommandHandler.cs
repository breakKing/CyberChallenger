using Mediator;
using Shared.Infrastructure.RelationalDatabase.Interfaces;
using TeamService.Persistence;
using TeamService.Persistence.Entities;

namespace TeamService.Features.Crud.Create;

public sealed class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, CreateTeamResponse>
{
    private readonly ICrudService _crudService;

    public CreateTeamCommandHandler(ICrudService crudService)
    {
        _crudService = crudService;
    }

    /// <inheritdoc />
    public async ValueTask<CreateTeamResponse> Handle(CreateTeamCommand command, CancellationToken ct)
    {
        var team = new Team(command.Name, command.GameId);

        await _crudService.CreateTeamAsync(team, ct);

        return new(team.Id);
    }
}