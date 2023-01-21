using FastEndpoints.Swagger;
using GatewayApi.Common.Auth;
using GatewayApi.Common.Grpc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;
using Shared.Contracts.IdentityProviderService;

namespace GatewayApi.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddFastEndpoints();

        services.AddSwaggerDoc(settings =>
        {
            settings.Title = "CyberChallenger API";
            settings.Version = "v1";

            settings.AddAuth(AuthConstants.JwtDisplayName, new()
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Description = "Введите JWT-токен (без префикса Bearer)",
                Name = AuthConstants.JwtTokenHeader,
                In = OpenApiSecurityApiKeyLocation.Header,
            });
            
            settings.AddAuth(AuthConstants.FingerprintDisplayName, new()
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Description = "Введите fingerprint (уникально идентифицирующий клиента)",
                Name = AuthConstants.UserAgentFingerprintHeader,
                In = OpenApiSecurityApiKeyLocation.Header,
            });
            
        }, addJWTBearerAuth: false);
        
        return services;
    }

    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServicesOptions>(configuration.GetSection(ServicesOptions.SectionName));
        var servicesOptions = new ServicesOptions();
        configuration.Bind(ServicesOptions.SectionName, servicesOptions);
        
        services.AddGrpcClientWithHeader<IdentityManager.IdentityManagerClient>(servicesOptions.IdentityProviderService);
        services.AddGrpcClientWithHeader<TokenManager.TokenManagerClient>(servicesOptions.IdentityProviderService);

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(auth =>
        {
            auth.AddScheme<CustomAuthHandler>(JwtBearerDefaults.AuthenticationScheme, AuthConstants.JwtDisplayName);

            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        
        return services;
    }
}