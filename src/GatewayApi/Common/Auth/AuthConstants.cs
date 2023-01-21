namespace GatewayApi.Common.Auth;

public static class AuthConstants
{
    public const string JwtDisplayName = "Custom JWT token authentication";
    public const string FingerprintDisplayName = "Fingerprint to support custom JWT token authentication";
    
    
    /// <summary>
    /// Header, содержащий уникальный fingerprint, генерируемый клиентом самостоятельно
    /// </summary>
    public const string UserAgentFingerprintHeader = "X-Client-Fingerprint";
    
    /// <summary>
    /// Header, содержащий JWT-токен
    /// </summary>
    public const string JwtTokenHeader = "X-JWT-Token";
}