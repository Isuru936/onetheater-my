using System.Text;
using Microsoft.OpenApi.Models;

namespace OneTheater.API.Extensions;

internal static class SwaggerExtensions
{

    private static readonly string[] OAuthScopes = { "email", "openid", "profile", "offline_access" };
    private static readonly string[] SecurityScopes = { "openid", "profile", "email" };

    internal static IServiceCollection AddSwaggerDocumentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Use external URL for Swagger OAuth2 (browser-accessible)
        string externalRealmUrl = GetExternalRealmUrl(configuration);
        var authorizationUrl = new Uri($"{externalRealmUrl}/protocol/openid-connect/auth");
        var tokenUrl = new Uri($"{externalRealmUrl}/protocol/openid-connect/token");

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "OneTheater API",
                Version = "v1",
                Description = "OneTheater API secured with Keycloak and OAuth2 Authorization Code flow."
            });

            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));

            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = authorizationUrl,
                        TokenUrl = tokenUrl,
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID Connect scope" },
                            { "profile", "User profile information" },
                            { "email", "User email address" },
                            { "offline_access", "Refresh token access" }
                        },
                        RefreshUrl = tokenUrl
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "OAuth2"
                        }
                    },
                    OAuthScopes
                }
            });
        });

        return services;
    }

    internal static WebApplication AddSwaggerUIOidcConfiguration(
        this WebApplication app,
        IConfiguration configuration)
    {
        string clientId = configuration["Keycloak:Resource"];
        string clientSecret = configuration["Keycloak:Credentials:Secret"];
        string realm = configuration["Keycloak:Realm"];
        string appName = configuration["Keycloak:Resource"];

        if (string.IsNullOrWhiteSpace(clientId) ||
            string.IsNullOrWhiteSpace(clientSecret) ||
            string.IsNullOrWhiteSpace(realm) ||
            string.IsNullOrWhiteSpace(appName))
        {
            return app;
        }

        app.UseSwaggerUI(options =>
        {
            options.OAuthClientId(clientId);

            if (app.Environment.IsDevelopment())
            {
                options.OAuthClientSecret(clientSecret);
            }

            options.OAuthAppName(appName);
            options.OAuthScopeSeparator(" ");
            options.OAuthScopes(SecurityScopes);
        });

        return app;
    }

    private static string GetExternalRealmUrl(IConfiguration configuration)
    {
        string? realm = configuration["Keycloak:Realm"];
        string? externalAuthServerUrl = configuration["Keycloak:ExternalAuthServerUrl"];

        if (string.IsNullOrWhiteSpace(realm) || string.IsNullOrWhiteSpace(externalAuthServerUrl))
        {
            throw new InvalidOperationException(
                "Keycloak:ExternalAuthServerUrl or Keycloak:Realm is missing in configuration.");
        }

        return $"{externalAuthServerUrl.TrimEnd('/')}/realms/{realm}";
    }
}
