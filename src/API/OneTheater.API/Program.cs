using OneTheater.API;
using OneTheater.API.Extensions;
using OneTheater.Common.Application;
using OneTheater.Common.Infrastructure;
using OneTheater.Modules.Users.Infrastructure;
using OneTheater.Modules.Shows.Infrastructure;
using OneTheater.Common.Presentation.Endpoints;
using Serilog;
using OneTheater.API.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplication([
    OneTheater.Modules.Users.Application.AssemblyReference.Assembly,
    OneTheater.Modules.Shows.Application.AssemblyReference.Assembly]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddInfrastructure(
    [ShowsModule.ConfigureConsumers],
    databaseConnectionString,
    redisConnectionString);

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString);

builder.Configuration.AddModuleConfiguartion(["users", "shows"]);

builder.Services.AddUsersModules(builder.Configuration);
builder.Services.AddShowsModules(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapEndpoints();

app.MapHealthChecks("health", new HealthCheckOptions()
{ 
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.Run();
