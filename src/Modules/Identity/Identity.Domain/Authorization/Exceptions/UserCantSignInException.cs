using System.Security.Claims;

namespace Identity.Domain.Authorization.Exceptions;

public sealed class UserCantSignInException : Exception
{
    public ClaimsPrincipal Principal { get; private set; }

    /// <inheritdoc />
    public UserCantSignInException(ClaimsPrincipal principal) :
        base($"User with provided principal was not able to sign in")
    {
        Principal = principal;
    }
}