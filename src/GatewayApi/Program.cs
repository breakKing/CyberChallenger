using GatewayApi.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Dependency injection

services.AddMainServices();
services.AddMicroservices(configuration);
services.AddAuth(configuration);
services.AddRedisCache(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();