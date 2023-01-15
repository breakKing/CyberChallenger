using Shared.Infrastructure.Persistence.Extensions;
using Shared.Infrastructure.Persistence.Interfaces;

namespace IdentityProviderService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRelationalDatabase<IdentityContext>(configuration.GetConnectionString("Database")!);

        var unitOfWork = services.FirstOrDefault(
            s => s.ServiceType == typeof(IGenericUnitOfWork<IdentityContext>));

        if (unitOfWork is not null)
        {
            services.Remove(unitOfWork);
        }

        return services;
    }
}