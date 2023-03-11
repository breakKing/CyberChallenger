using Shared.Infrastructure.EventSourcing.Kafka.Extensions;
using TournamentService.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Dependency injection

// services.AddMainServices();
services.AddInfrastructure(configuration);
// services.AddFeatures();

var app = builder.Build();

// App pipeline

app.UseKafkaBus();
// app.MapGrpcServices();
app.MapGet("/", () => "Grpc is running...");

app.Run();