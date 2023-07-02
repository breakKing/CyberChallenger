using Common.Application.Primitives;
using Identity.Domain.Authorization.Exceptions;
using LanguageExt.Common;

namespace Identity.Application.Authorization.Refresh;

public sealed class RefreshCommandHandler : ICommandHandler<RefreshCommand, RefreshResponse>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IClaimsPrincipalService _claimsPrincipalService;

    public RefreshCommandHandler(
        IAuthorizationService authorizationService, 
        IClaimsPrincipalService claimsPrincipalService)
    {
        _authorizationService = authorizationService;
        _claimsPrincipalService = claimsPrincipalService;
    }

    /// <inheritdoc />
    public async Task<Result<RefreshResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var principal = request.Principal;
        var user = await _claimsPrincipalService.FindUserByPrincipalAsync(principal, cancellationToken);
        
        if (user is null)
        {
            var exception = new UserForPrincipalNotFoundException(principal);
            return new Result<RefreshResponse>(exception);
        }
        
        if (!await _authorizationService.CanSignInAsync(user, cancellationToken))
        {
            var exception = new UserCantSignInException(principal);
            return new Result<RefreshResponse>(exception);
        }

        var claimsPrincipal = await _claimsPrincipalService.CreateClaimsPrincipalAsync(user, cancellationToken);

        var result = new OidcResult
        {
            Succeeded = true,
            Principal = claimsPrincipal
        };

        return new RefreshResponse(result);
    }
}