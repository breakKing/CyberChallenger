using System.Net;
using Shared.Contracts.GatewayApi.Auth.Logout;

namespace GatewayApi.Endpoints.Auth.Logout;

public sealed class LogoutEndpointSummary : EndpointSummaryBase
{
    private readonly LogoutResponse _successResponseExample = new(true);
    
    public LogoutEndpointSummary()
    {
        Summary = "Выход из системы";
        Description = "Выход из системы и отзыв токенов";
        
        AddSuccessResponseExample(HttpStatusCode.OK, _successResponseExample);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}