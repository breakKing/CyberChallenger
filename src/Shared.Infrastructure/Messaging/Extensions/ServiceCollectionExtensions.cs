using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Messaging.Configuration;

namespace Shared.Infrastructure.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessagingWithEntityFrameworkCore<TContext>(
        this IServiceCollection services, 
        IConfiguration configuration)
        where TContext : DbContext
    {
        var config = configuration.GetMessagingConfig();
        
        services.AddCap(cap =>
        {
            cap.UseEntityFramework<TContext>(ef =>
            {
                ef.Schema = "messaging";
            });

            cap.Configure(config);
        });
        
        return services;
    }
    
    public static IServiceCollection AddMessaging(
        this IServiceCollection services, 
        IConfiguration configuration,
        string dbConnectionString)
    {
        var config = configuration.GetMessagingConfig();
        
        services.AddCap(cap =>
        {
            cap.UsePostgreSql(pgsql =>
            {
                pgsql.ConnectionString = dbConnectionString;
                pgsql.Schema = "messaging";
            });

            cap.Configure(config);
        });
        
        return services;
    }

    public static MessagingConfiguration GetMessagingConfig(this IConfiguration configuration)
    {
        return configuration.GetSection(MessagingConfiguration.SectionName).Get<MessagingConfiguration>()!;
    }

    private static void Configure(this CapOptions cap, MessagingConfiguration config)
    {
        cap.UseKafka(config.KafkaAddress);

        cap.ConsumerThreadCount = config.ConsumerThreadCount;
        cap.ProducerThreadCount = config.ProducerThreadCount;

        cap.DefaultGroupName = config.ConsumerGroupName;

        cap.FailedMessageExpiredAfter = config.FailedMessageExpiredAfterSeconds;
        cap.SucceedMessageExpiredAfter = config.SucceedMessageExpiredAfterSeconds;

        cap.FailedRetryCount = config.FailedRetryCount;
        cap.FailedRetryInterval = config.FailedRetryIntervalSeconds;
            
        cap.CollectorCleaningInterval = config.CleanupIntervalSeconds;

        cap.UseStorageLock = true;
    }
}