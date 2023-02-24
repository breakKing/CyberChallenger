using System.Net;
using GatewayApi.Services.Interfaces;
using Shared.Contracts.GatewayApi.Auth.Refresh;
using IMapper = MapsterMapper.IMapper;

namespace GatewayApi.Endpoints.Auth.Refresh;

public sealed class RefreshEndpoint : EndpointWithoutRequestBase<RefreshResponse>
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    
    /// <inheritdoc />
    public RefreshEndpoint(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }
    
    /// <inheritdoc />
    public override void Configure()
    {
        Post("refresh");
        
        Group<LoginGroup>();
        
        AllowAnonymous();

        ConfigureSwaggerDescription(new RefreshEndpointSummary(),
            HttpStatusCode.OK, 
            HttpStatusCode.BadRequest, 
            HttpStatusCode.InternalServerError);
    }
    
    /// <inheritdoc />
    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var accessToken = HttpContext.Request
            .Headers["Authorization"]
            .ToString()[7..];

        var result = await _authService.RefreshAsync(accessToken, ct);

        var task = result.Match<Task>(
            success =>
                SendDataAsync(_mapper.Map<RefreshResponse>(success), ct: ct),
            error => 
                SendErrorsAsync(_mapper.Map<List<string>>(error), ct: ct)
        );

        await task;
    }
}