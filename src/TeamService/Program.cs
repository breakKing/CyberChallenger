using Shared.Grpc;
using Shared.Infrastructure.EventSourcing.Kafka.Extensions;
using TeamService.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Dependency injection

services.AddMainServices();
services.AddInfrastructure(configuration);
services.AddFeatures();

var app = builder.Build();

// App pipeline

app.UseKafkaBus();
app.MapGrpcServices();
app.MapGet("/", () => "Grpc is running...");

app.Run();