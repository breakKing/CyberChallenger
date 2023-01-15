using IdentityProviderService.Common.Services;
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
        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<User>(o =>
            {
                o.SignIn.RequireConfirmedAccount = false;
                o.User.RequireUniqueEmail = true;
                o.Password.RequiredLength = 8;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<IdentityContext>();

        services.Replace(ServiceDescriptor.Transient(typeof(IUserStore<>), typeof(CustomUserStore)));
        services.Replace(ServiceDescriptor.Transient(typeof(IRoleStore<>), typeof(CustomRoleStore)));

        return services;
    }
}