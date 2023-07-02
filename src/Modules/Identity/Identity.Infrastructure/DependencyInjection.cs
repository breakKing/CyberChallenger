using Identity.Domain.Authorization.Entities;
using Identity.Domain.Identity.Entities;
using Identity.Infrastructure.Authorization;
using Identity.Infrastructure.Authorization.Configuration;
using Identity.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

namespace Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration,
        OpenIdRoutes openIdRoutes)
    {
        services.AddPersistence(configuration.GetConnectionString("Database")!);
        services.AddOpenIdInfrastructure(configuration, openIdRoutes);
        services.AddIdentity();
        
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IdentityContext>(contextOptions =>
        {
            contextOptions.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("migrations_history", "migrations");
                npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            contextOptions.UseSnakeCaseNamingConvention();

            contextOptions.UseOpenIddict<
                Domain.Authorization.Entities.Application, 
                Domain.Authorization.Entities.Authorization, 
                Scope, 
                Token, 
                Guid>();
        });

        return services;
    }
    
    private static IServiceCollection AddOpenIdInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration,
        OpenIdRoutes openIdRoutes)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });
        
        services.AddOptions<AuthOptions>()
            .Bind(configuration.GetRequiredSection(AuthOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var authOptions = new AuthOptions();
        configuration.Bind(AuthOptions.SectionName, authOptions);
        
        var jwtOptions = authOptions.Jwt;
        
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<IdentityContext>()
                    .ReplaceDefaultEntities<
                        Domain.Authorization.Entities.Application, 
                        Domain.Authorization.Entities.Authorization, 
                        Scope, 
                        Token, 
                        Guid>();
            })
            .AddServer(options =>
            {
                options.SetTokenEndpointUris(openIdRoutes.Token)
                    .SetRevocationEndpointUris(openIdRoutes.Revocation)
                    .SetUserinfoEndpointUris(openIdRoutes.UserInfo)
                    .SetIntrospectionEndpointUris(openIdRoutes.Introspection);
                
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
}