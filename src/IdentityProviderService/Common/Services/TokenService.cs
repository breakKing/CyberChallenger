using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using IdentityProviderService.Common.Extensions;
using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Common.Models;
using IdentityProviderService.Persistence.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityProviderService.Common.Services;

public sealed class TokenService : ITokenService
{
    private const string UserIdClaimType = "user-id";
    
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly ILogger<TokenService> _logger;
    
    private readonly SigningCredentials _signingCredentials;
    private readonly TokenValidationParameters _validationParameters;

    public TokenService(IOptions<JwtOptions> jwtOptions, ILogger<TokenService> logger)
    {
        _jwtOptions = jwtOptions;
        _logger = logger;

        var rsa = RSA.Create();
        rsa.ImportKeyFromPemFile(jwtOptions.Value.IssuerSigningPrivateKeyFile);
        var rsaPrivateKey = new RsaSecurityKey(rsa);
        _signingCredentials = new SigningCredentials(
            key: rsaPrivateKey,
            algorithm: SecurityAlgorithms.RsaSha256);
        
        rsa = RSA.Create();
        rsa.ImportKeyFromPemFile(jwtOptions.Value.IssuerSigningPublicKeyFile);
        var rsaPublicKey = new RsaSecurityKey(rsa);

        _validationParameters = new()
        {
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = rsaPublicKey,
            RequireAudience = jwtOptions.Value.RequireAudience,
            RequireExpirationTime = jwtOptions.Value.RequireExpirationTime,
            RequireSignedTokens = jwtOptions.Value.RequireSignedTokens,
            ValidateAudience = jwtOptions.Value.ValidateAudience,
            ValidateIssuer = jwtOptions.Value.ValidateIssuer,
            ValidateIssuerSigningKey = jwtOptions.Value.ValidateIssuerSigningKey,
            ValidateLifetime = jwtOptions.Value.ValidateLifetime,
            ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 },
            ValidAudience = jwtOptions.Value.ValidAudience,
            ValidIssuer = jwtOptions.Value.ValidIssuer
        };
    }

    /// <inheritdoc />
    public string GenerateAccessTokenAsync(User user)
    {
        return GenerateToken(user, _jwtOptions.Value.AccessTokenExpirationTimeInMinutes);
    }

    /// <inheritdoc />
    public string GenerateRefreshTokenAsync(User user)
    {
        return GenerateToken(user, _jwtOptions.Value.RefreshTokenExpirationTimeInMinutes);
    }

    /// <inheritdoc />
    public bool ValidateToken(string token)
    {
        ClaimsPrincipal? claimsPrincipal = null;
        try
        {
            claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, _validationParameters, out _);
        }
        catch(Exception ex)
        {
            _logger.LogInformation("Exception occurred while validating token: {@Exception}", ex.Message);
        }
        
        return claimsPrincipal is not null;
    }

    /// <inheritdoc />
    public Guid? GetUserIdFromToken(string token)
    {
        ClaimsPrincipal? claimsPrincipal = null;
        try
        {
            claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, _validationParameters, out _);
            var idAsString = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == UserIdClaimType)?.Value;
            if (Guid.TryParse(idAsString, out var id))
            {
                return id;
            }
        }
        catch(Exception ex)
        {
            _logger.LogInformation("Exception occurred while obtaining user id token: {@Exception}", ex.Message);
        }

        return null;
    }

    private string GenerateToken(User user, long expirationTimeInMinutes)
    {
        var now = DateTime.UtcNow;
            
        var jwt = new JwtSecurityToken(
            audience: _jwtOptions.Value.ValidAudience,
            issuer: _jwtOptions.Value.ValidIssuer,
            claims: new[]
            {
                new Claim("user-id", user.Id.ToString()),
            },
            notBefore: now,
            expires: now.AddMinutes(expirationTimeInMinutes),
            signingCredentials: _signingCredentials
        );
            
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return token;
    }
}