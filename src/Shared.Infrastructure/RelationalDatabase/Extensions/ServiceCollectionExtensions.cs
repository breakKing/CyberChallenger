using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.RelationalDatabase.Implementations;
using Shared.Infrastructure.RelationalDatabase.Interceptors;
using Shared.Infrastructure.RelationalDatabase.Interfaces;

namespace Shared.Infrastructure.RelationalDatabase.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация сервисов DbContext, UnitOfWork и Repository
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <param name="optionsAction"></param>
    /// <typeparam name="TContext"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddRelationalDatabase<TContext>(this IServiceCollection services, 
        string connectionString, Action<DbContextOptionsBuilder>? optionsAction = null)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(contextOptions =>
        {
            contextOptions.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("migrations_history", "migrations");
                npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            contextOptions.UseSnakeCaseNamingConvention();
            contextOptions.AddInterceptors(new SoftDeleteInterceptor(), new AuditInterceptor());

            optionsAction?.Invoke(contextOptions);
        });
        
        services.AddScoped<IGenericUnitOfWork, EntityFrameworkCoreUnitOfWork<TContext>>();

        return services;
    }
}