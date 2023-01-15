using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessTokenAsync(User user);
    string GenerateRefreshTokenAsync(User user);
    bool ValidateToken(string token);
}