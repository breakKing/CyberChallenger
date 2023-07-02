using Common.Application.PipelineBehaviors;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<IApplicationAssemblyMarker>();

            config.Lifetime = ServiceLifetime.Scoped;
            
            config.NotificationPublisher = new TaskWhenAllPublisher();

            config.AddOpenBehavior(typeof(ExceptionPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
        });
        
        return services;
    }
}