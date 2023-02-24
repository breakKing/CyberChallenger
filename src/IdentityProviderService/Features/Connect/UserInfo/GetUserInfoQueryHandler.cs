using Mediator;

namespace IdentityProviderService.Features.Connect.UserInfo;

public sealed class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, GetUserInfoResponse>
{
    private readonly IClaimsPrincipalService _claimsPrincipalService;

    public GetUserInfoQueryHandler(IClaimsPrincipalService claimsPrincipalService)
    {
        _claimsPrincipalService = claimsPrincipalService;
    }

    /// <inheritdoc />
    public ValueTask<GetUserInfoResponse> Handle(GetUserInfoQuery query, CancellationToken ct)
    {
        var claimsPrincipal = query.Principal;

        if (claimsPrincipal is null)
        {
            return ValueTask.FromResult(new GetUserInfoResponse(null));
        }
        
        var userInfo = _claimsPrincipalService.GetUserInfoFromPrincipal(claimsPrincipal);

        return ValueTask.FromResult(new GetUserInfoResponse(userInfo));
    }
}