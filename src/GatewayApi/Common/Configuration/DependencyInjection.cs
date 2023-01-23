using FastEndpoints.Swagger;
using GatewayApi.Common.Grpc;

namespace GatewayApi.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.AddHttpContextAccessor();

        services.AddSwaggerDoc(settings =>
        {
            settings.Title = "CyberChallenger API";
            settings.Version = "v1";

            // settings.AddAuth(AuthConstants.JwtDisplayName, new()
            // {
            //     Type = OpenApiSecuritySchemeType.ApiKey,
            //     Description = "Введите JWT-токен (без префикса Bearer)",
            //     Name = AuthConstants.JwtTokenHeader,
            //     In = OpenApiSecurityApiKeyLocation.Header,
            // });
            //
            // settings.AddAuth(AuthConstants.FingerprintDisplayName, new()
            // {
            //     Type = OpenApiSecuritySchemeType.ApiKey,
            //     Description = "Введите fingerprint (уникально идентифицирующий клиента)",
            //     Name = AuthConstants.UserAgentFingerprintHeader,
            //     In = OpenApiSecurityApiKeyLocation.Header,
            // });
            
        }, addJWTBearerAuth: false);
        
        return services;
    }

    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServicesOptions>(configuration.GetSection(ServicesOptions.SectionName));
        var servicesOptions = new ServicesOptions();
        configuration.Bind(ServicesOptions.SectionName, servicesOptions);
        
        

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        
        
        return services;
    }
}