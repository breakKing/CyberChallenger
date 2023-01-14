using Mediator;

namespace TeamService.Features.Crud.Create;

public sealed record CreateTeamCommand(string Name, Guid GameId) : ICommand<CreateTeamResponse>;