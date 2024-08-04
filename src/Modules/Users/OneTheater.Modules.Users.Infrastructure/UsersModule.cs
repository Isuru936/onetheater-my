using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using OneTheater.Common.Application.Abstrations.Data;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.Infrastructure.Database;
using OneTheater.Modules.Users.Infrastructure.Users;
using OneTheater.Modules.Users.Presentation.Users;

namespace OneTheater.Modules.Users.Infrastructure;
public static class UsersModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        UsersEndpoints.MapEndPoints(app);
    }

    public static IServiceCollection AddUsersModules(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<UsersDbContext>(options =>
        {
            options
            .UseNpgsql(databaseConnectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
            .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());
    }
}
