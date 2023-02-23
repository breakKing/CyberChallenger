using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace IdentityProviderService.Common.Helpers;

public static class RsaHelper
{
    public static RsaSecurityKey ImportKeyFromPemFile(string pathToFile)
    {
        var rsa = RSA.Create();
        var keyBytes = File.ReadAllText(pathToFile).ToCharArray();
        rsa.ImportFromPem(keyBytes);

        return new RsaSecurityKey(rsa);
    }
}