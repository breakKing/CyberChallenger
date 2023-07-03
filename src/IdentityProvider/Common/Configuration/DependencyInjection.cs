using System.Reflection;
using IdentityProviderService.Common.Constants;
using IdentityProviderService.Common.Helpers;
using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Common.Models;
using IdentityProviderService.Common.Services;
using IdentityProviderService.Features.Connect;
using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;
using Mapster;
using MapsterMapper;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

namespace IdentityProviderService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration.GetConnectionString("Database")!);
        services.AddOpenIdInfrastructure(configuration);
        services.AddIdentity();

        return services;
    }

    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();

        services.AddSingleton<IBootstrapService, BootstrapService>();

        return services;
    }

    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddMediator(opt =>
        {
            opt.ServiceLifetime = ServiceLifetime.Scoped;
        });
        
        services.AddMapping(Assembly.GetExecutingAssembly());

        services.AddOpenIdConnectFeatures();

        return services;
    }

    private static IServiceCollection AddOpenIdInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });
        
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetRequiredSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtOptions = new JwtOptions();
        configuration.Bind(JwtOptions.SectionName, jwtOptions);
        
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<IdentityContext>()
                    .ReplaceDefaultEntities<Application, Authorization, Scope, Token, Guid>();
            })
            .AddServer(options =>
            {
                options.SetTokenEndpointUris(OpenIdRoutes.Token)
                    .SetRevocationEndpointUris(OpenIdRoutes.Revocation)
                    .SetUserinfoEndpointUris(OpenIdRoutes.UserInfo)
                    .SetIntrospectionEndpointUris(OpenIdRoutes.Introspection);
                
                options.AddSigningKey(RsaHelper.ImportKeyFromPemFile(jwtOptions.IssuerSigningPrivateKeyFile));
                options.AddEncryptionKey(RsaHelper.ImportKeyFromPemFile(jwtOptions.IssuerEncryptionPrivateKeyFile));

                options.AllowPasswordFlow()
                    .AllowRefreshTokenFlow();

                options.UseReferenceAccessTokens()
                    .UseReferenceRefreshTokens();

                options.DisableScopeValidation();
                
                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough();
                
                options.Configure(cfg =>
                {
                    cfg.AccessTokenLifetime = TimeSpan.FromMinutes(jwtOptions.AccessTokenExpirationTimeInMinutes);
                    cfg.RefreshTokenLifetime = TimeSpan.FromMinutes(jwtOptions.RefreshTokenExpirationTimeInMinutes);
                    cfg.TokenValidationParameters.ValidAudience = jwtOptions.ValidAudience;
                    cfg.TokenValidationParameters.ValidIssuer = jwtOptions.ValidIssuer;
                    cfg.TokenValidationParameters.ValidateLifetime = jwtOptions.ValidateLifetime;
                    cfg.TokenValidationParameters.ValidateAudience = jwtOptions.ValidateAudience;
                    cfg.TokenValidationParameters.ValidateIssuer = jwtOptions.ValidateIssuer;
                    cfg.TokenValidationParameters.RequireAudience = jwtOptions.RequireAudience;
                    cfg.TokenValidationParameters.RequireExpirationTime = jwtOptions.RequireExpirationTime;
                    cfg.TokenValidationParameters.RequireSignedTokens = jwtOptions.RequireSignedTokens;
                    cfg.TokenValidationParameters.ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey;
                });
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();

                options.EnableAuthorizationEntryValidation();
            });

        return services;
    }
    
    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(o =>
            {
                o.SignIn.RequireConfirmedAccount = false;
                
                o.User.RequireUniqueEmail = true;
                
                o.Password.RequiredLength = 8;
                o.Password.RequireNonAlphanumeric = false;
                
                o.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                o.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                o.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
                o.ClaimsIdentity.EmailClaimType = OpenIddictConstants.Claims.Email;
            })
            .AddEntityFrameworkStores<IdentityContext>();

        return services;
    }
    
    private static IServiceCollection AddMapping(this IServiceCollection services, params Assembly[] assemblies)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(assemblies);
        
        var mapper = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapper);
        
        return services;
    }
}