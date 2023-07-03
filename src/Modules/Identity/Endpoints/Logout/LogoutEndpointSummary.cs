using System.Net;
using Common.Presentation.Abstractions;

namespace Identity.Endpoints.Logout;

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