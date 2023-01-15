using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Common.Models;

namespace IdentityProviderService.Common.Services;

public sealed class SessionManager : ISessionManager
{
    private readonly ITokenService _tokenService;

    public SessionManager(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<SessionOperationResult> CreateSessionAsync(Guid userId, string userAgentFingerprint,
        CancellationToken ct = default)
    {
        return null;
    }

    /// <inheritdoc />
    public async Task<SessionOperationResult> RenewSessionAsync(Guid userId, string userAgentFingerprint,
        string refreshToken, CancellationToken ct = default)
    {
        return null;
    }

    /// <inheritdoc />
    public async Task DropSessionAsync(Guid userId, string userAgentFingerprint, CancellationToken ct = default)
    {
    }
}