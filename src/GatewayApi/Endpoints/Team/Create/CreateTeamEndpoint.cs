using System.Net;
using Shared.Contracts.GatewayApi.Base;
using Shared.Contracts.GatewayApi.Team;

namespace GatewayApi.Endpoints.Team.Create;

public sealed class CreateTeamEndpoint : EndpointBase<CreateTeamRequest, CreateTeamResponse>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("teams");
        
        // TODO убрать после добавления авторизации
        AllowAnonymous();
        
        Description(desc =>
        {
            desc.Accepts<CreateTeamRequest>("application/json");
            desc.Produces<ApiResponse<CreateTeamResponse>>((int)HttpStatusCode.Created);
            desc.Produces<ApiResponse<CreateTeamResponse>>((int)HttpStatusCode.BadRequest);
            desc.Produces<ApiResponse<CreateTeamResponse>>((int)HttpStatusCode.InternalServerError);
        }, clearDefaults: true);
        
        Summary(summary =>
        {
            summary.Summary = "Создание команды в определённой дисциплине (игре)";
            summary.Description = "Создание команды в определённой дисциплине (игре)";
        });
    }
    
    /// <inheritdoc />
    public override async Task HandleAsync(CreateTeamRequest req, CancellationToken ct)
    {
        await base.HandleAsync(req, ct);
    }
}