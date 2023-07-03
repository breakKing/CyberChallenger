using System.Security.Claims;
using IdentityProvider.Features.Connect.UserInfo;
using IdentityProvider.Persistence.Entities;

namespace IdentityProvider.Features.Connect;

public interface IClaimsPrincipalService
{
    Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(User user, CancellationToken ct = default);
    UserInfoDto GetUserInfoFromPrincipal(ClaimsPrincipal principal);
}