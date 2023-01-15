using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Common.Services;
using IdentityProviderService.Features.Identity;
using IdentityProviderService.Features.Tokens;
using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityProviderService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddIdentity(configuration);

        return services;
    }

    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddIdentityFeatures();
        services.AddTokensFeatures();
        
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

        services.AddSingleton<ITokenService, TokenService>();
        services.AddTransient<ISessionManager, SessionManager>();

        return services;
    }
}