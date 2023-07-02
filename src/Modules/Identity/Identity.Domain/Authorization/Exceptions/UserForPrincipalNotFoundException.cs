using System.Security.Claims;

namespace Identity.Domain.Authorization.Exceptions;

public sealed class UserForPrincipalNotFoundException : Exception
{
    public ClaimsPrincipal? Principal { get; private set; }

    /// <inheritdoc />
    public UserForPrincipalNotFoundException(ClaimsPrincipal? principal) :
        base($"Invalid refresh token was provided for principal")
    {
        Principal = principal;
    }
}