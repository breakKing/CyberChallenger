using System.Text.Json;
using Common.Presentation.Primitives;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace App;

public static class PipelineConfiguration
{
    public static void Configure(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseFastEndpoints(config =>
        {
            config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            config.Endpoints.RoutePrefix = "api";
            
            config.Errors.ResponseBuilder = (failures, _, _) =>
            {
                return new ApiResponse<object>(null, true,
                    failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}").ToList());
            };
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerGen();
        }
    }
}