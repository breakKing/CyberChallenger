using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Common.Services;

public sealed class TokenService : ITokenService
{
    /// <inheritdoc />
    public string GenerateAccessTokenAsync(User user)
    {
        return null;
    }

    /// <inheritdoc />
    public string GenerateRefreshTokenAsync(User user)
    {
        return null;
    }

    /// <inheritdoc />
    public string ValidateToken(string token)
    {
        return null;
    }
}