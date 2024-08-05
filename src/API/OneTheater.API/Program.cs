using OneTheater.API;
using OneTheater.API.Extensions;
using OneTheater.Common.Application;
using OneTheater.Common.Infrastructure;
using OneTheater.Modules.Users.Infrastructure;
using OneTheater.Modules.Shows.Infrastructure;
using OneTheater.Common.Presentation.Endpoints;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplication([
    OneTheater.Modules.Users.Application.AssemblyReference.Assembly,
    OneTheater.Modules.Shows.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(
    [ShowsModule.ConfigureConsumers],
    builder.Configuration.GetConnectionString("Database")!,
    builder.Configuration.GetConnectionString("Cache")!);

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

app.UseSerilogRequestLogging();

app.Run();
