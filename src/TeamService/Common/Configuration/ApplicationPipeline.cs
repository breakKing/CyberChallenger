using TeamService.Features.Crud;

namespace TeamService.Common.Configuration;

public static class ApplicationPipeline
{
    public static WebApplication MapGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<TeamCrudGrpcService>();

        return app;
    }
}