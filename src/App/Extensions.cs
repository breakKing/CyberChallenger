using System.Text.Json;
using Common.Presentation.Contracts;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace App;

public static class Extensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddFastEndpoints();

        builder.Services.SwaggerDocument(settings =>
        {
            settings.DocumentSettings = documentSettings =>
            {
                documentSettings.Title = "CyberChallenger API";
                documentSettings.Version = "v1";
            };
        });
    }

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