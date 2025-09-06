using System.Configuration;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Kiota;
using Keycloak.AuthServices.Sdk.Kiota.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using OneTheater.Common.Application.Abstractions;

namespace OneTheater.Common.Infrastructure.Keycloak;

public static class KeycloakServiceExtensions
{
    public static IServiceCollection AddKeycloakService(this IServiceCollection services, IConfiguration configuration)
    {

        string keycloakAdminApiTokenClient = "KeycloakAdminAPITokenClient";
        KeycloakAdminClientOptions keycloakAdminClientOptions = configuration.
            GetKeycloakOptions<KeycloakAdminClientOptions>("KeycloakAdminAPI")!;
        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(
            keycloakAdminApiTokenClient,
            client =>
            {
                client.ClientId = keycloakAdminClientOptions.Resource;
                client.ClientSecret = keycloakAdminClientOptions.Credentials.Secret;
                // Prefer internal URL for server-to-server auth if available, otherwise fall back to external
                string? realm = configuration["KeycloakAdminAPI:realm"];
                string? internalUrl = configuration["KeycloakAdminAPI:internal-auth-server-url"];
                string? externalUrl = configuration["KeycloakAdminAPI:auth-server-url"];
                string? baseUrl = string.IsNullOrWhiteSpace(internalUrl) ? externalUrl : internalUrl;
                client.TokenEndpoint = $"{baseUrl!.TrimEnd('/')}/realms/{realm}/protocol/openid-connect/token";
            });

        services
            .AddKeycloakAdminHttpClient(keycloakAdminClientOptions)
            .AddClientCredentialsTokenHandler(keycloakAdminApiTokenClient);

        services.AddScoped<IKeycloakService, KeycloakService>();

        return services;
    }
}
