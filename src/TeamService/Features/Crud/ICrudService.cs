using TeamService.Persistence.Entities;

namespace TeamService.Features.Crud;

public interface ICrudService
{
    Task CreateTeamAsync(Team team, CancellationToken ct = default);
}