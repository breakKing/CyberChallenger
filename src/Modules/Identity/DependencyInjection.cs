using Identity.Common.Configuration;
using Identity.Common.Constants;
using Identity.Common.Services.Implementations;
using Identity.Common.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;

namespace Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuth(configuration)
            .AddRedisCache(configuration);
    }
    
    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AuthOptions>()
            .Bind(configuration.GetRequiredSection(AuthOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        var authOptions = new AuthOptions();
        configuration.Bind(AuthOptions.SectionName, authOptions);

        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        services.AddAuthorization();
        
        services.AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer(authOptions.IdentityProviderAddress);
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
            client.BaseAddress = new Uri(authOptions.IdentityProviderAddress);
        });
        
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    private static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(redis =>
        {
            redis.Configuration = configuration["Redis:Host"];
            redis.InstanceName = configuration["Redis:Instance"];
        });

        services.AddScoped<IRedisCacheService, RedisCacheService>();
        
        return services;
    }
}