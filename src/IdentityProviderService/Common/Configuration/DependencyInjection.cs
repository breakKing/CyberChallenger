﻿using System.Reflection;
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
using OpenIddict.Validation.AspNetCore;

namespace IdentityProviderService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration.GetConnectionString("Database")!);
        services.AddOpenIdInfrastructure(configuration);
        services.AddIdentity(configuration);

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

        var jwtOptions = configuration.GetValue<JwtOptions>(JwtOptions.SectionName)!;
        
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
                    .SetLogoutEndpointUris(OpenIdRoutes.Logout)
                    .SetUserinfoEndpointUris(OpenIdRoutes.UserInfo);
                
                options.AddSigningKey(RsaHelper.ImportKeyFromPemFile(jwtOptions.IssuerSigningPrivateKeyFile));
                options.DisableAccessTokenEncryption();

                options.AllowPasswordFlow()
                    .AllowRefreshTokenFlow();

                options.DisableScopeValidation();
                
                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
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
    
    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, Role>(o =>
            {
                o.SignIn.RequireConfirmedAccount = false;
                o.User.RequireUniqueEmail = true;
                o.Password.RequiredLength = 8;
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