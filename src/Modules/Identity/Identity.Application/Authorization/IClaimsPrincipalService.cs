using System.Security.Claims;
using Identity.Domain.Identity.Entities;

namespace Identity.Application.Authorization;

public interface IClaimsPrincipalService
{
    Task<User?> FindUserByPrincipalAsync(ClaimsPrincipal principal, CancellationToken ct = default);
    
    UserInfoDto GetUserInfoFromPrincipal(ClaimsPrincipal principal);
    
    Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(User user, CancellationToken ct = default);
}