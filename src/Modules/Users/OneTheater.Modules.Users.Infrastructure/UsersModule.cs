using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneTheater.Common.Infrastructure.Interceptors;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.Infrastructure.Database;
using OneTheater.Modules.Users.Infrastructure.Users;
using OneTheater.Modules.Users.Infrastructure.UserTypes;

namespace OneTheater.Modules.Users.Infrastructure;
public static class UsersModule
{
    public static IServiceCollection AddUsersModules(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<UsersDbContext>((sp, options) =>
        {
            options
            .UseNpgsql(databaseConnectionString,
                npgsqlOptions => npgsqlOptions
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>());
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTypeRepository, UserTypeRepository>();

    }
}
