using System.Security.Claims;

namespace IdentityProvider.Features.Connect;

public sealed class OidcResult
{
    public bool Succeeded { get; set; }
    public string Error { get; set; } = string.Empty;
    public string ErrorDescription { get; set; } = string.Empty;
    public ClaimsPrincipal? Principal { get; set; }

    public OidcResult() {}

    public OidcResult(string error, string errorDesc)
    {
        Succeeded = false;
        Error = error;
        ErrorDescription = errorDesc;
    }
}
