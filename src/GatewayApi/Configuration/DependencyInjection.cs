using FastEndpoints.Security;
using FastEndpoints.Swagger;

namespace GatewayApi.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        
        services.AddSwaggerDoc(settings =>
        {
            settings.Title = "CyberChallenger GatewayApi";
            settings.Version = "v1";
        });
        
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddJWTBearerAuth("temp_key_for_signing");
        
        return services;
    }
}