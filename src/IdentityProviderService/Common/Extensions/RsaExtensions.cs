using System.Security.Cryptography;

namespace IdentityProviderService.Common.Extensions;

public static class RsaExtensions
{
    public static void ImportKeyFromPemFile(this RSA rsa, string pathToFile)
    {
        var keyBytes = File.ReadAllText(pathToFile);
        rsa.ImportFromPem(keyBytes);
    }
}