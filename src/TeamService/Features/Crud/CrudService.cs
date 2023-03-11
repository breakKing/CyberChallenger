using Shared.Contracts.TeamService;
using Shared.Infrastructure.EventSourcing.Services.Interfaces;
using Shared.Infrastructure.Persistence.Interfaces;
using TeamService.Persistence.Entities;

namespace TeamService.Features.Crud;

public sealed class CrudService : ICrudService
{
    private readonly IGenericUnitOfWork _unitOfWork;
    private readonly IEventProducer _eventProducer;

    public CrudService(IGenericUnitOfWork unitOfWork, IEventProducer eventProducer)
    {
        _unitOfWork = unitOfWork;
        _eventProducer = eventProducer;
    }

    /// <inheritdoc />
    public async Task CreateTeamAsync(Team team, CancellationToken ct = default)
    {
        await _unitOfWork.StartTransactionAsync(ct);
        
        _unitOfWork.Repository<Team>().AddOne(team);

        await _eventProducer.ProduceAsync(
            "team_producer",
            "team",
            team.Id.ToString(),
            new TeamCreatedGrpcEventV1
            {
                Id = team.Id.ToString(),
                Name = team.Name
            }, 
            ct: ct);
            
        await _unitOfWork.CommitAsync(ct);
    }
}