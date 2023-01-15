using IdentityProviderService.Common.Models;
using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Common.Interfaces;

public interface ISessionManager
{
    Task<bool> SessionExistsAsync(Guid userId, string userAgentFingerprint, CancellationToken ct = default);

    Task<Session?> GetSessionAsync(Guid userId, string userAgentFingerprint, CancellationToken ct = default);
    
    Task<SessionOperationResult> CreateSessionAsync(User user, string userAgentFingerprint, 
        CancellationToken ct = default);
    
    Task<SessionOperationResult> RenewSessionAsync(User user, string userAgentFingerprint, string refreshToken,
        CancellationToken ct = default);

    Task DropSessionAsync(User user, string userAgentFingerprint, CancellationToken ct = default);
}