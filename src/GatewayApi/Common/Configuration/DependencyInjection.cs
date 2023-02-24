using System.Reflection;
using FastEndpoints.Swagger;
using GatewayApi.Common.Constants;
using GatewayApi.Common.Models.Options;
using GatewayApi.Services.Implementations;
using GatewayApi.Services.Interfaces;
using Mapster;
using MapsterMapper;
using OpenIddict.Validation.AspNetCore;

namespace GatewayApi.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.AddHttpContextAccessor();
        services.AddMapping(Assembly.GetExecutingAssembly());

        services.AddSwaggerDoc(settings =>
        {
            settings.Title = "CyberChallenger API";
            settings.Version = "v1";
        });

        return services;
    }

    public static IServiceCollection AddMicroservices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ServicesOptions>()
            .Bind(configuration.GetRequiredSection(ServicesOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        var servicesOptions = new ServicesOptions();
        configuration.Bind(ServicesOptions.SectionName, servicesOptions);

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AuthOptions>()
            .Bind(configuration.GetRequiredSection(AuthOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        var authOptions = new AuthOptions();
        configuration.Bind(AuthOptions.SectionName, authOptions);
        
        var servicesOptions = new ServicesOptions();
        configuration.Bind(ServicesOptions.SectionName, servicesOptions);
        
        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        services.AddAuthorization();
        
        services.AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer(servicesOptions.IdentityProviderService);
                options.AddAudiences(authOptions.Audience);
                
                options.UseIntrospection()
                    .SetClientId(authOptions.ClientId)
                    .SetClientSecret(authOptions.ClientSecret);

                options.UseSystemNetHttp();
                
                options.UseAspNetCore();
            });

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddHttpClient(HttpClientNames.IdentityProviderService, client =>
        {
            client.BaseAddress = new Uri(servicesOptions.IdentityProviderService);
        });
        
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(redis =>
        {
            redis.Configuration = configuration["Redis:Host"];
            redis.InstanceName = configuration["Redis:Instance"];
        });

        services.AddScoped<IRedisCacheService, RedisCacheService>();
        
        return services;
    }
    
    private static IServiceCollection AddMapping(this IServiceCollection services, params Assembly[] assemblies)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(assemblies);
        
        var mapper= new Mapper(typeAdapterConfig);
        services.AddSingleton<MapsterMapper.IMapper>(mapper);
        
        return services;
    }
}