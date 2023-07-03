using System.Net;
using Common.Presentation.Abstractions;
using FastEndpoints;
using Identity.Common.Services.Interfaces;

namespace Identity.Endpoints.Login;

public sealed class LoginEndpoint : EndpointBase<LoginRequest, LoginResponse>
{
    private readonly IAuthService _authService;

    /// <inheritdoc />
    public LoginEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("login");
        
        Group<AuthGroup>();
        
        AllowAnonymous(Http.POST);

        ConfigureSwaggerDescription(new LoginEndpointSummary(),
            HttpStatusCode.OK, 
            HttpStatusCode.BadRequest, 
            HttpStatusCode.InternalServerError);
    }
    
    /// <inheritdoc />
    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(req.Login, req.Password, ct);

        var task = result.Match<Task>(
            success =>
                SendDataAsync(
                    new LoginResponse(success.UserId, success.AccessToken, success.ExpiresIn), 
                    ct: ct),
            error =>
                SendErrorsAsync(error.Error, ct: ct)
        );

        await task;
    }
}