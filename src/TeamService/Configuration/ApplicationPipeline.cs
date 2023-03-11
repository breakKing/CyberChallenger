using TeamService.Features.Crud;

namespace TeamService.Configuration;

public static class ApplicationPipeline
{
    public static WebApplication MapGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<TeamCrudGrpcService>();

        return app;
    }
}