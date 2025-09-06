using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace OneTheater.API.Extensions;

internal static class AuthenticationExtensions
{
	public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddKeycloakWebApiAuthentication(configuration, options =>
		{
			// Allow HTTP authority in development scenarios
			options.RequireHttpsMetadata = false;
		});

		// Dynamically adjust Authority (issuer) for container vs local execution
		services.PostConfigureAll<JwtBearerOptions>(options =>
		{
			string? realm = configuration["Keycloak:realm"];
			string? resource = configuration["Keycloak:resource"];
			string? internalUrl = configuration["Keycloak:internal-auth-server-url"];
			string? externalUrl = configuration["Keycloak:auth-server-url"];

			string? baseUrl = !string.IsNullOrWhiteSpace(internalUrl) ? internalUrl : externalUrl;

			if (!string.IsNullOrWhiteSpace(baseUrl) && !string.IsNullOrWhiteSpace(realm))
			{
				string internalAuthority = $"{(string.IsNullOrWhiteSpace(internalUrl) ? baseUrl : internalUrl).TrimEnd('/')}/realms/{realm}";
				string? externalAuthority = !string.IsNullOrWhiteSpace(externalUrl) ? $"{externalUrl.TrimEnd('/')}/realms/{realm}" : null;

				// Use internal authority for discovery/validation in container network
				options.Authority = internalAuthority;
				options.RequireHttpsMetadata = internalAuthority.StartsWith("https", StringComparison.OrdinalIgnoreCase);
				options.MetadataAddress = $"{baseUrl.TrimEnd('/')}/realms/{realm}/.well-known/openid-configuration";
				options.RefreshOnIssuerKeyNotFound = true;

				// Configure configuration manager explicitly to avoid stale keys
				options.ConfigurationManager = new Microsoft.IdentityModel.Protocols.ConfigurationManager<Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration>(
					options.MetadataAddress!,
					new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfigurationRetriever(),
					new Microsoft.IdentityModel.Protocols.HttpDocumentRetriever { RequireHttps = false });

				// Accept tokens issued by either internal or external authorities
				var validIssuers = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { internalAuthority };
				if (!string.IsNullOrWhiteSpace(externalAuthority))
				{
					validIssuers.Add(externalAuthority);
				}
				options.TokenValidationParameters.ValidIssuers = validIssuers;

				// Resolve signing keys from internal certs endpoint to bypass wrong jwks_uri in metadata
				string certsEndpoint = $"{(string.IsNullOrWhiteSpace(internalUrl) ? baseUrl : internalUrl)!.TrimEnd('/')}/realms/{realm}/protocol/openid-connect/certs";
				options.TokenValidationParameters.IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
				{
					try
					{
						using var http = new System.Net.Http.HttpClient();
						string jwksJson = http.GetStringAsync(certsEndpoint).GetAwaiter().GetResult();
						var jwks = new Microsoft.IdentityModel.Tokens.JsonWebKeySet(jwksJson);
						return jwks.Keys;
					}
					catch
					{
						return System.Array.Empty<Microsoft.IdentityModel.Tokens.SecurityKey>();
					}
				};
			}

			// Ensure audiences are matched: include Keycloak clientId (resource) and any configured audiences
			var audiences = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (!string.IsNullOrWhiteSpace(resource))
			{
				audiences.Add(resource);
				options.Audience = resource;
			}

			string[]? schemeAudiences = configuration
				.GetSection("Authentication:Schemes:Bearer:ValidAudiences")
				.Get<string[]>();
			if (schemeAudiences != null)
			{
				foreach (string aud in schemeAudiences)
				{
					if (!string.IsNullOrWhiteSpace(aud))
					{
						audiences.Add(aud);
					}
				}
			}

			string[]? tokenParamAudiences = configuration
				.GetSection("Authentication:Schemes:Bearer:TokenValidationParameters:ValidAudiences")
				.Get<string[]>();
			if (tokenParamAudiences != null)
			{
				foreach (string aud in tokenParamAudiences)
				{
					if (!string.IsNullOrWhiteSpace(aud))
					{
						audiences.Add(aud);
					}
				}
			}

			if (audiences.Count > 0)
			{
				options.TokenValidationParameters.ValidAudiences = audiences;
			}

			bool? validateAudience = configuration
				.GetValue<bool?>("Authentication:Schemes:Bearer:TokenValidationParameters:ValidateAudience");
			options.TokenValidationParameters.ValidateAudience = !validateAudience.HasValue || validateAudience.Value;
		});

		services.AddAuthorization(options =>
		{
			options.AddPolicy("AdminOnly", policy =>
				policy.RequireRole("admin"));

			options.AddPolicy("ClientOrAdmin", policy =>
				policy.RequireRole("client", "admin"));
		});

		return services;
	}
}
