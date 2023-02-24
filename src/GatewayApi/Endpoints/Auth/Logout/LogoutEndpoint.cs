using System.Net;
using GatewayApi.Services.Interfaces;
using Shared.Contracts.GatewayApi.Auth.Logout;
using IMapper = MapsterMapper.IMapper;

namespace GatewayApi.Endpoints.Auth.Logout;

public sealed class LogoutEndpoint : EndpointWithoutRequestBase<LogoutResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public LogoutEndpoint(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
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
                SendDataAsync(_mapper.Map<LogoutResponse>(success), ct: ct),
            error => 
                SendErrorsAsync(_mapper.Map<List<string>>(error), ct: ct)
        );

        await task;
    }
}