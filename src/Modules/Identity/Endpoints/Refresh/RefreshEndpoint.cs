using System.Net;
using Common.Presentation.Abstractions;
using FastEndpoints;
using Identity.Common.Services.Interfaces;

namespace Identity.Endpoints.Refresh;

public sealed class RefreshEndpoint : EndpointWithoutRequestBase<RefreshResponse>
{
    private readonly IAuthService _authService;
    
    /// <inheritdoc />
    public RefreshEndpoint(IAuthService authService)
    {
        _authService = authService;
    }
    
    /// <inheritdoc />
    public override void Configure()
    {
        Post("refresh");
        
        Group<AuthGroup>();
        
        AllowAnonymous();

        ConfigureSwaggerDescription(new RefreshEndpointSummary(),
            HttpStatusCode.OK, 
            HttpStatusCode.BadRequest, 
            HttpStatusCode.InternalServerError);
    }
    
    /// <inheritdoc />
    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        if (!HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            await SendErrorsAsync("You don't have any access token to refresh", ct: ct);
            return;
        }
        
        var accessToken = authHeader.ToString()[7..];

        var result = await _authService.RefreshAsync(accessToken, ct);

        var task = result.Match<Task>(
            success =>
                SendDataAsync(
                    new RefreshResponse(success.AccessToken, success.ExpiresIn), 
                    ct: ct),
            error => 
                SendErrorsAsync(error.Error, ct: ct)
        );

        await task;
    }
}