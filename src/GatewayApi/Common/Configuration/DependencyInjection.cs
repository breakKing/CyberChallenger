using FastEndpoints.Swagger;
using GatewayApi.Common.Models;
using OpenIddict.Validation.AspNetCore;

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
        });
        
        return services;
    }

    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
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
        
        return services;
    }
}