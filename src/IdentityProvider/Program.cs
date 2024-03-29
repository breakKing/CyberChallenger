using IdentityProvider.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Dependency injection

services.AddMainServices();
services.AddInfrastructure(configuration);
services.AddFeatures();

var app = builder.Build();

// App pipeline

app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();

await app.BootstrapBeforeRunAsync();
app.Run();