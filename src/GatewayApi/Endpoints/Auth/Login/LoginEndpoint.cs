using System.Net;
using GatewayApi.Services.Interfaces;
using Shared.Contracts.GatewayApi.Auth.Login;
using IMapper = MapsterMapper.IMapper;

namespace GatewayApi.Endpoints.Auth.Login;

public sealed class LoginEndpoint : EndpointBase<LoginRequest, LoginResponse>
{
    private readonly IOpenIdClient _openIdClient;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public LoginEndpoint(IOpenIdClient openIdClient, IMapper mapper)
    {
        _openIdClient = openIdClient;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("login");
        
        Group<LoginGroup>();
        
        AllowAnonymous();

        ConfigureSwaggerDescription(new LoginEndpointSummary(),
            HttpStatusCode.OK, 
            HttpStatusCode.BadRequest, 
            HttpStatusCode.InternalServerError);
    }
    
    /// <inheritdoc />
    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var result = await _openIdClient.LoginAsync(req.Login, req.Password, ct);

        var task = result.Match<Task>(
            success =>
                SendDataAsync(_mapper.Map<LoginResponse>(success), ct: ct),
            error =>
                SendErrorsAsync(_mapper.Map<List<string>>(error), ct: ct)
        );

        await task;
    }
}