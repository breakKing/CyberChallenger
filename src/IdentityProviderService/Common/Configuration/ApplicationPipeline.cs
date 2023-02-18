using IdentityProviderService.Common.Interfaces;

namespace IdentityProviderService.Common.Configuration;

public static class ApplicationPipeline
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
    
    public static WebApplication BootstrapBeforeRun(this WebApplication app)
    {
        var bootstrapService = app.Services.GetRequiredService<IBootstrapService>();
        bootstrapService.BootstrapAsync();

        return app;
    }
}