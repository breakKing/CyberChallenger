namespace IdentityProviderService.Common.Configuration;

public static class ApplicationPipeline
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}