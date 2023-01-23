using IdentityProviderService.Common.Helpers;
using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddOpenId(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<IdentityContext>()
                    .ReplaceDefaultEntities<Application, Authorization, Scope, Token, Guid>();
            })
            .AddServer(options =>
            {
                options.SetTokenEndpointUris("connect/token");
                options.SetAuthorizationEndpointUris("connect/authorize");
                options.SetLogoutEndpointUris("connect/logout");
                options.SetUserinfoEndpointUris("connect/userinfo");
                
                // TODO норм прокинуть файл
                options.AddSigningKey(RsaHelper.ImportKeyFromPemFile("RsaKeys/jwt_signing_private_key.pem"));
                options.DisableAccessTokenEncryption();

                options.UseReferenceAccessTokens();
                options.UseReferenceRefreshTokens();

                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();

                options.DisableScopeValidation();

                // Register the ASP.NET Core host and configure the ASP.NET Core options.
                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();

                options.EnableAuthorizationEntryValidation();
                options.EnableTokenEntryValidation();
            });

        return services;
    }
}