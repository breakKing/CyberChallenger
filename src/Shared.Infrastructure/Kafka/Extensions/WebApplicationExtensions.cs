using KafkaFlow;
using Microsoft.AspNetCore.Builder;

namespace Shared.Infrastructure.Kafka.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseKafkaBus(this WebApplication app)
    {
        var bus = app.Services.CreateKafkaBus();
        app.Lifetime.ApplicationStarted.Register(() => bus.StartAsync(app.Lifetime.ApplicationStopped));
        
        return app;
    }
}