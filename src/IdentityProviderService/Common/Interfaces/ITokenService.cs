using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessTokenAsync(User user);
    string GenerateRefreshTokenAsync(User user);
    string ValidateToken(string token);
}