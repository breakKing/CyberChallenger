using IdentityProviderService.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Dependency injection

services.AddMainServices();
services.AddInfrastructure(configuration);
services.AddFeatures();

var app = builder.Build();

// App pipeline

app.MapEndpoints();

app.Run();