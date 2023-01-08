using TeamService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Grpc is running...");

app.Run();