using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Common.Models;
using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SessionOptions = IdentityProviderService.Common.Models.SessionOptions;

namespace IdentityProviderService.Common.Services;

public sealed class SessionManager : ISessionManager
{
    private readonly ITokenService _tokenService;
    private readonly IdentityContext _context;
    private readonly IOptions<SessionOptions> _sessionOptions;

    public SessionManager(ITokenService tokenService, IdentityContext context, IOptions<SessionOptions> sessionOptions)
    {
        _tokenService = tokenService;
        _context = context;
        _sessionOptions = sessionOptions;
    }

    /// <inheritdoc />
    public async Task<SessionOperationResult> CreateSessionAsync(User user, string userAgentFingerprint,
        CancellationToken ct = default)
    {
        var sessions = await _context.Sessions.Where(
            s => s.UserId == user.Id && s.Fingerprint == userAgentFingerprint)
            .ToListAsync(ct);

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        
        if (sessions.Count >= _sessionOptions.Value.MaxConcurrentSessions)
        {
            _context.RemoveRange(sessions);
        }
        
        var accessToken = _tokenService.GenerateAccessTokenAsync(user);
        var refreshToken = _tokenService.GenerateRefreshTokenAsync(user);

        var session = new Session
        {
            UserId = user.Id,
            RefreshToken = refreshToken,
            Fingerprint = userAgentFingerprint,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresInMinutes = _sessionOptions.Value.SessionLifetimeInMinutes
        };
        
        await _context.Sessions.AddAsync(session, ct);
        await _context.SaveChangesAsync(ct);

        await transaction.CommitAsync(ct);

        return new SessionOperationResult(true, accessToken, refreshToken);
    }

    /// <inheritdoc />
    public async Task<SessionOperationResult> RenewSessionAsync(User user, string userAgentFingerprint,
        string refreshToken, CancellationToken ct = default)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(
            s => s.UserId == user.Id && s.Fingerprint == userAgentFingerprint, ct);

        if (session is null)
        {
            return new SessionOperationResult(false, string.Empty, string.Empty);
        }

        if (!_tokenService.ValidateToken(refreshToken) || refreshToken != session.RefreshToken || session.Expired)
        {
            return new SessionOperationResult(false, string.Empty, string.Empty);
        }

        var accessToken = _tokenService.GenerateAccessTokenAsync(user);
        var newRefreshToken = _tokenService.GenerateRefreshTokenAsync(user);
        
        session.RefreshToken = newRefreshToken;
        session.ExpiresInMinutes = _sessionOptions.Value.SessionLifetimeInMinutes;

        _context.Sessions.Update(session);
        await _context.SaveChangesAsync(ct);
        
        return new SessionOperationResult(true, accessToken, newRefreshToken);
    }

    /// <inheritdoc />
    public async Task DropSessionAsync(User user, string userAgentFingerprint, CancellationToken ct = default)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(
            s => s.UserId == user.Id && s.Fingerprint == userAgentFingerprint, ct);

        if (session is null)
        {
            return;
        }
        
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(ct);
    }
}