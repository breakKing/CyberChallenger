using Shared.Infrastructure.Persistence.Interfaces;
using TeamService.Persistence;
using TeamService.Persistence.Entities;

namespace TeamService.Features.Crud;

public sealed class CrudService : ICrudService
{
    private readonly IGenericUnitOfWork<TeamContext> _unitOfWork;

    public CrudService(IGenericUnitOfWork<TeamContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task CreateTeamAsync(Team team, CancellationToken ct = default)
    {
        await _unitOfWork.StartTransactionAsync(ct);
        _unitOfWork.Repository<Team>().AddOne(team);
        await _unitOfWork.CommitAsync(ct);
    }
}