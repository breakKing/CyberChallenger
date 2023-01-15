using System.Security.Cryptography;

namespace IdentityProviderService.Common.Extensions;

public static class RsaExtensions
{
    public static void ImportRsaPublicKeyFromFile(this RSA rsa, string pathToFile)
    {
        var keyBytes = File.ReadAllBytes(pathToFile);
        rsa.ImportRSAPublicKey(keyBytes, out _);
    }
    
    public static void ImportRsaPrivateKeyFromFile(this RSA rsa, string pathToFile)
    {
        var keyBytes = File.ReadAllBytes(pathToFile);
        rsa.ImportRSAPrivateKey(keyBytes, out _);
    }
}