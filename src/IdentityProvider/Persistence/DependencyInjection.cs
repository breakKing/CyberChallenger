using IdentityProviderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IdentityContext>(contextOptions =>
        {
            contextOptions.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("migrations_history", "migrations");
                npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            contextOptions.UseSnakeCaseNamingConvention();

            contextOptions.UseOpenIddict<Application, Authorization, Scope, Token, Guid>();
        });

        return services;
    }
}