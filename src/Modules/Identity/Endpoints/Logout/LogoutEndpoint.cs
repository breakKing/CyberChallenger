using System.Net;
using Common.Presentation.Abstractions;
using FastEndpoints;
using Identity.Common.Services.Interfaces;

namespace Identity.Endpoints.Logout;

public sealed class LogoutEndpoint : EndpointWithoutRequestBase<LogoutResponse>
{
    private readonly IAuthService _authService;

    public LogoutEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("logout");
        
        Group<AuthGroup>();

        ConfigureSwaggerDescription(new LogoutEndpointSummary(),
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

        var result = await _authService.LogoutAsync(accessToken, ct);
        
        var task = result.Match<Task>(
            success =>
                SendDataAsync(new LogoutResponse(true), ct: ct),
            error => 
                SendErrorsAsync(error.Error, ct: ct)
        );

        await task;
    }
}