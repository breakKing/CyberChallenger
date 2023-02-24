namespace GatewayApi.Endpoints.Auth;

public sealed class AuthGroup : Group
{
    public AuthGroup()
    {
        Configure("auth", ep => ep.Tags("Auth"));
    }
}