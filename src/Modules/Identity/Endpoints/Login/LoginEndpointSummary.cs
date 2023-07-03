using System.Net;
using Common.Presentation.Abstractions;

namespace Identity.Endpoints.Login;

public sealed class LoginEndpointSummary : EndpointSummaryBase
{
    private readonly LoginResponse _successResponseExample = 
        new(Guid.NewGuid().ToString(), "access-token-here", 30);
    
    public LoginEndpointSummary()
    {
        Summary = "Логин в системе";
        Description = "Логин в системе";
        
        AddSuccessResponseExample(HttpStatusCode.OK, _successResponseExample);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}