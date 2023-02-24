namespace GatewayApi.Endpoints.Auth;

public sealed class LoginGroup : Group
{
    public LoginGroup()
    {
        Configure("auth", ep => ep.Tags("Auth"));
    }
}