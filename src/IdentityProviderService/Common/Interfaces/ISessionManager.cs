using IdentityProviderService.Common.Models;

namespace IdentityProviderService.Common.Interfaces;

public interface ISessionManager
{
    Task<SessionOperationResult> CreateSessionAsync(Guid userId, string userAgentFingerprint, 
        CancellationToken ct = default);
    
    Task<SessionOperationResult> RenewSessionAsync(Guid userId, string userAgentFingerprint, string refreshToken,
        CancellationToken ct = default);

    Task DropSessionAsync(Guid userId, string userAgentFingerprint, CancellationToken ct = default);
}