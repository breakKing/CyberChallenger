using TeamService.Common;
using TeamService.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Dependency injection

services.AddGrpc();
services.AddMainServices();
services.AddInfrastructure(configuration);
services.AddFeatures();

var app = builder.Build();

// App pipeline

app.MapGrpcServices();
app.MapGet("/", () => "Grpc is running...");

app.Run();