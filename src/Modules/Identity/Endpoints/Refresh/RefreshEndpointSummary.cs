using System.Net;
using Common.Presentation.Abstractions;

namespace Identity.Endpoints.Refresh;

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