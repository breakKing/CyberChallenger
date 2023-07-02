using Identity.Domain.Identity.Entities;

namespace Identity.Application.Authorization;

public interface IAuthorizationService
{
    Task<User?> FindUserByLoginAsync(string login, CancellationToken ct = default);

    Task<bool> CheckPasswordSignInAsync(User user, string password, CancellationToken ct = default);
    
    Task<bool> CanSignInAsync(User user, CancellationToken ct = default);
}