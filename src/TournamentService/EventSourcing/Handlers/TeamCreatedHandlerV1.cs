using KafkaFlow;
using KafkaFlow.TypedHandler;
using Shared.Contracts.TeamService;

namespace TournamentService.EventSourcing.Handlers;

public sealed class TeamCreatedHandlerV1 : IMessageHandler<TeamCreatedGrpcEventV1>
{
    private readonly ILogger<TeamCreatedHandlerV1> _logger;

    public TeamCreatedHandlerV1(ILogger<TeamCreatedHandlerV1> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task Handle(IMessageContext context, TeamCreatedGrpcEventV1 message)
    {
        _logger.LogInformation("Consumed message {@Message}", message);
    }
}