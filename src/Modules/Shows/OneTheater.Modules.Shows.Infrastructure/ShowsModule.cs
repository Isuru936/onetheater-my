using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneTheater.Common.Infrastructure.Interceptors;
using OneTheater.Common.Infrastructure.Outbox;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Customers;
using OneTheater.Modules.Shows.Domain.Movies;
using OneTheater.Modules.Shows.Domain.Screens;
using OneTheater.Modules.Shows.Domain.Seats;
using OneTheater.Modules.Shows.Domain.SeatsInventories;
using OneTheater.Modules.Shows.Domain.Shows;
using OneTheater.Modules.Shows.Domain.Theaters;
using OneTheater.Modules.Shows.Infrastructure.Customers;
using OneTheater.Modules.Shows.Infrastructure.Database;
using OneTheater.Modules.Shows.Infrastructure.Movies;
using OneTheater.Modules.Shows.Infrastructure.Outbox;
using OneTheater.Modules.Shows.Infrastructure.PublicApi;
using OneTheater.Modules.Shows.Infrastructure.Screens;
using OneTheater.Modules.Shows.Infrastructure.Seats;
using OneTheater.Modules.Shows.Infrastructure.SeatsInventories;
using OneTheater.Modules.Shows.Infrastructure.Shows;
using OneTheater.Modules.Shows.Infrastructure.Theaters;
using OneTheater.Modules.Shows.Presentation.Consumers;
using OneTheater.Modules.Shows.PublicApi;

namespace OneTheater.Modules.Shows.Infrastructure;
public static class ShowsModule
{
    public static IServiceCollection AddShowsModules(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        services.AddInfrastructure(configuration);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurtor)
    { 
        registrationConfigurtor.AddConsumer<UserCreatedIntegrationEventConsumer>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ShowsDbContext>((sp, options) =>
        {
            options
            .UseNpgsql(databaseConnectionString,
                npgsqlOptions => npgsqlOptions
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Shows))
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>());
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ShowsDbContext>());

        services.AddScoped<ICustomersApi, CustomersApi>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ITheaterRepository, TheaterRepository>();
        services.AddScoped<IScreenRepository, ScreenRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();
        services.AddScoped<ISeatsInventoryRepository, SeatsInventoryRepository>();
        services.AddScoped<IShowRepository, ShowRepository>();

        services.Configure<OutboxOptions>(configuration.GetSection("Shows:Outbox"));
        services.ConfigureOptions<ConfigureProcessOutboxJob>();
    }
}
