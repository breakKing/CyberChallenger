using System.Net;
using Shared.Contracts.GatewayApi.Team;

namespace GatewayApi.Endpoints.Team.Create;

public sealed class CreateTeamEndpointSummary : EndpointSummaryBase
{
    private readonly CreateTeamResponse _successResponseExample = new(Guid.NewGuid());
    
    public CreateTeamEndpointSummary()
    {
        Summary = "Создание команды для дисциплины (игры)";
        Description = "Создание команды для дисциплины (игры)";
        
        AddSuccessResponseExample(HttpStatusCode.Created, _successResponseExample);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}