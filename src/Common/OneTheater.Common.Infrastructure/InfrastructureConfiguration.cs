using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using OneTheater.Common.Application.Abstrations.Data;
using OneTheater.Common.Application.Caching;
using OneTheater.Common.Application.Clock;
using OneTheater.Common.Application.EventBus;
using OneTheater.Common.Infrastructure.Caching;
using OneTheater.Common.Infrastructure.Clock;
using OneTheater.Common.Infrastructure.Data;
using OneTheater.Common.Infrastructure.Interceptors;
using OneTheater.Common.Infrastructure.Keycloak;
using OneTheater.Common.Infrastructure.Outbox;
using Quartz;
using StackExchange.Redis;

namespace OneTheater.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string databaseConnectionString,
        string redisConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        services.TryAddSingleton<ICacheService, CacheService>();

        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
            });
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        services.AddKeycloakService(configuration);

        services.AddMassTransit(registration =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers)
            {
                configureConsumer(registration);
            }

            registration.UsingRabbitMq((context, cfg) =>
            {
                string host = configuration["MessageBroker:Host"] ?? "rabbitmq://onetheater.rabbitmq";
                string username = configuration["MessageBroker:Username"] ?? "guest";
                string password = configuration["MessageBroker:Password"] ?? "guest";

                cfg.Host(new Uri(host), h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
