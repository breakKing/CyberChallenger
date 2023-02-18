using IdentityProviderService.Common.Interfaces;

namespace IdentityProviderService.Common.Configuration;

public static class ApplicationPipeline
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
    
    public static async Task<WebApplication> BootstrapBeforeRunAsync(this WebApplication app)
    {
        var bootstrapService = app.Services.GetRequiredService<IBootstrapService>();
        await bootstrapService.BootstrapAsync();

        return app;
    }
}