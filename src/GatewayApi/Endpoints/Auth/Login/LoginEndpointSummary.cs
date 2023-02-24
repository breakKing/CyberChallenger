using System.Net;
using Shared.Contracts.GatewayApi.Auth.Login;

namespace GatewayApi.Endpoints.Auth.Login;

public sealed class LoginEndpointSummary : EndpointSummaryBase
{
    private readonly LoginResponse _successResponseExample = 
        new(Guid.NewGuid().ToString(), "access-token-here", 30);
    
    public LoginEndpointSummary()
    {
        Summary = "Логин в системе";
        Description = "Логин в системе";
        
        AddSuccessResponseExample(HttpStatusCode.Created, _successResponseExample);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}