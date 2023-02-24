using System.Net;
using Shared.Contracts.GatewayApi.Auth.Refresh;

namespace GatewayApi.Endpoints.Auth.Refresh;

public sealed class RefreshEndpointSummary : EndpointSummaryBase
{
    private readonly RefreshResponse _successResponseExample = new("access-token-here", 30);
    
    public RefreshEndpointSummary()
    {
        Summary = "Обновление токенов пользователя в системе";
        Description = "Обновление токенов пользователя в системе";
        
        AddSuccessResponseExample(HttpStatusCode.OK, _successResponseExample);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}