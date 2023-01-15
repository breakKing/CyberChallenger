using System.Reflection;
using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Common.Models;
using IdentityProviderService.Common.Services;
using IdentityProviderService.Features.Identity;
using IdentityProviderService.Features.Tokens;
using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SessionOptions = IdentityProviderService.Common.Models.SessionOptions;

namespace IdentityProviderService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddIdentity(configuration);
        services.AddJwt(configuration);

        return services;
    }

    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddMediator(opt =>
        {
            opt.ServiceLifetime = ServiceLifetime.Scoped;
        });
        services.AddMapping(Assembly.GetExecutingAssembly());
        
        services.AddIdentityFeatures();
        services.AddTokensFeatures();
        
        return services;
    }
    
    private static IServiceCollection AddMapping(this IServiceCollection services, params Assembly[] assemblies)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(assemblies);
        
        var mapper= new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapper);
        
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

        services.Replace(ServiceDescriptor.Transient(typeof(IUserStore<User>), typeof(CustomUserStore)));
        services.Replace(ServiceDescriptor.Transient(typeof(IRoleStore<Role>), typeof(CustomRoleStore)));
        
        services.AddScoped<ISessionManager, SessionManager>();
        services.Configure<SessionOptions>(configuration.GetSection(SessionOptions.SectionName));

        return services;
    }

    private static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        var jwtOptions = new JwtOptions();
        configuration.Bind(JwtOptions.SectionName, jwtOptions);

        if (string.IsNullOrEmpty(jwtOptions.IssuerSigningPublicKeyFile))
        {
            throw new InvalidOperationException("The JWT signing public key file name must be provided");
        }
        
        if (string.IsNullOrEmpty(jwtOptions.IssuerSigningPrivateKeyFile))
        {
            throw new InvalidOperationException("The JWT signing private key file name must be provided");
        }

        services.AddSingleton<ITokenService, TokenService>();
        
        return services;
    }
}