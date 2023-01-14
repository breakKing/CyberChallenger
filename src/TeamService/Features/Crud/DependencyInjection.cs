namespace TeamService.Features.Crud;

public static class DependencyInjection
{
    public static IServiceCollection AddCrudFeatures(this IServiceCollection services)
    {
        services.AddScoped<ICrudService, CrudService>();
        
        return services;
    }
}