using System.Net;
using Shared.Contracts.GatewayApi.Team;

namespace GatewayApi.Endpoints.Team.Create;

public sealed class CreateTeamEndpoint : EndpointBase<CreateTeamRequest, CreateTeamResponse>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("teams");
        
        Roles("superadmin");

        ConfigureSwaggerDescription(new CreateTeamEndpointSummary(),
            HttpStatusCode.Created, 
            HttpStatusCode.BadRequest, 
            HttpStatusCode.InternalServerError);
    }
    
    /// <inheritdoc />
    public override async Task HandleAsync(CreateTeamRequest req, CancellationToken ct)
    {
        await SendDataAsync(new CreateTeamResponse(new Guid()), HttpStatusCode.Created, ct);
    }
}