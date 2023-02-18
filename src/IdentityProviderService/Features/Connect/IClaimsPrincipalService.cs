using System.Security.Claims;
using IdentityProviderService.Features.Connect.UserInfo;
using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Features.Connect;

public interface IClaimsPrincipalService
{
    Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(User user, CancellationToken ct = default);
    UserInfoDto GetUserInfoFromPrincipal(ClaimsPrincipal principal);
}