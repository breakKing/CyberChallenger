using Common.Application.Primitives;
using Identity.Domain.Authorization.Exceptions;
using LanguageExt.Common;

namespace Identity.Application.Authorization.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IClaimsPrincipalService _claimsPrincipalService;

    public LoginCommandHandler(
        IAuthorizationService authorizationService, 
        IClaimsPrincipalService claimsPrincipalService)
    {
        _authorizationService = authorizationService;
        _claimsPrincipalService = claimsPrincipalService;
    }

    /// <inheritdoc />
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var login = request.Login;
        var password = request.Password;

        var user = await _authorizationService.FindUserByLoginAsync(login, cancellationToken);

        if (user is null)
        {
            var exception = new UserWithLoginNotFoundException(login);
            return new Result<LoginResponse>(exception);
        }

        var signInResult = await _authorizationService.CheckPasswordSignInAsync(user, password, cancellationToken);

        if (!signInResult)
        {
            var exception = new InvalidLoginException(login);
            return new Result<LoginResponse>(exception);
        }

        var claimsPrincipal = await _claimsPrincipalService.CreateClaimsPrincipalAsync(user, cancellationToken);

        var result = new OidcResult
        {
            Succeeded = true,
            Principal = claimsPrincipal
        };

        return new LoginResponse(result);
    }
}