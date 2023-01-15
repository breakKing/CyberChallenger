using IdentityProviderService.Common.Models;
using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Common.Interfaces;

public interface ISessionManager
{
    Task<SessionOperationResult> CreateSessionAsync(User user, string userAgentFingerprint, 
        CancellationToken ct = default);
    
    Task<SessionOperationResult> RenewSessionAsync(User user, string userAgentFingerprint, string refreshToken,
        CancellationToken ct = default);

    Task DropSessionAsync(User user, string userAgentFingerprint, CancellationToken ct = default);
}