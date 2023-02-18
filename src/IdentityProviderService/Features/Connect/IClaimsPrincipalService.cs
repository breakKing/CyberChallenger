using System.Security.Claims;
using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Features.Connect;

public interface IClaimsPrincipalService
{
    Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(User user, CancellationToken ct = default);
}