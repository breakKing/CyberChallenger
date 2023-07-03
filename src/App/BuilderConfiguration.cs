using FastEndpoints;
using FastEndpoints.Swagger;
using Identity;

namespace App;

public static class BuilderConfiguration
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

        builder.AddModules();
    }

    private static void AddModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityModule(builder.Configuration);
    }
}