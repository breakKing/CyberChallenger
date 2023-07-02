using Common.Application.Primitives;
using Identity.Domain.Authorization.Exceptions;
using LanguageExt.Common;

namespace Identity.Application.Authorization.UserInfo;

public sealed class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, GetUserInfoResponse>
{
    private readonly IClaimsPrincipalService _claimsPrincipalService;

    public GetUserInfoQueryHandler(IClaimsPrincipalService claimsPrincipalService)
    {
        _claimsPrincipalService = claimsPrincipalService;
    }
    
    /// <inheritdoc />
    public async Task<Result<GetUserInfoResponse>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var claimsPrincipal = request.Principal;

        if (claimsPrincipal is null)
        {
            var exception = new UserForPrincipalNotFoundException(claimsPrincipal);
            return new Result<GetUserInfoResponse>(exception);
        }
        
        var userInfo = _claimsPrincipalService.GetUserInfoFromPrincipal(claimsPrincipal);

        return new GetUserInfoResponse(userInfo);
    }
}