using IdentityProviderService.Features.Identity;
using IdentityProviderService.Features.Tokens;

namespace IdentityProviderService.Common.Configuration;

public static class ApplicationPipeline
{
    public static WebApplication MapGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<IdentityManagerGrpcService>();
        app.MapGrpcService<TokenManagerGrpcService>();

        return app;
    }
}