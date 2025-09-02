using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Keycloak.AuthServices.Sdk.Kiota.Admin;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OneTheater.Common.Application.Abstractions;
using OneTheater.Common.Domain.Abstractions;
using static Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.UsersRequestBuilder;

namespace OneTheater.Common.Infrastructure.Keycloak;

internal sealed class KeycloakService : IKeycloakService
{
    private readonly WithRealmItemRequestBuilder client;
    private readonly ILogger<KeycloakService> _logger;

    public KeycloakService(KeycloakAdminApiClient keycloakAdminApiClient, IConfiguration configuration, ILogger<KeycloakService> logger)
    {
        _logger = logger;
        client = keycloakAdminApiClient.Admin.Realms[configuration.GetSection("Keycloak:Realm").Value ?? "onetheater"];
    }

    public async Task<Result<string>> CreateUserAsync(CreateKeycloakUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if user already exists
            Result<string?> existingUserResult = await GetUserIdFromEmail(request.Email);
            if (existingUserResult.IsSuccess && existingUserResult.Value != null)
            {
                _logger.LogInformation("User with email {Email} already exists with ID {UserId}", request.Email, existingUserResult.Value);
                return Result.Success(existingUserResult.Value);
            }

            var userRepresentation = new UserRepresentation()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Enabled = true,
                EmailVerified = true,
            };

            await client.Users.PostAsync(userRepresentation, cancellationToken: cancellationToken);

            Result<string?> newUserResult = await GetUserIdFromEmail(request.Email);
            if (newUserResult.IsFailure || newUserResult.Value is null)
            {
                return Result.Failure<string>(Error.NullValue);
            }

            return Result.Success(newUserResult.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while creating Keycloak user {Username}", request.Username);
            return Result.Failure<string>(Error.Failure("Keycloak.Exception", ex.Message));
        }
    }

    ////private async Task<Result<string>> GetAdminAccessTokenAsync(CancellationToken cancellationToken)
    /// <summary>
    /// {
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    //    try
    ///    {
    //        // Use InternalAuthServerUrl for container-to-container communication, fallback to ExternalAuthServerUrl
    ////        string authServerUrl = _configuration["Keycloak:InternalAuthServerUrl"] ?? _configuration["Keycloak:ExternalAuthServerUrl"] ?? throw new InvalidOperationException("Keycloak auth server URL not configured");
    ////        string tokenUrl = $"{authServerUrl.TrimEnd('/')}/realms/{_realm}/protocol/openid-connect/token";

    //        var formParams = new List<KeyValuePair<string, string>>
    ////        {
    //            new("grant_type", "client_credentials"),
    //            new("client_id", _clientId),
    //            new("client_secret", _clientSecret)
    ////        };

    ////        using var content = new FormUrlEncodedContent(formParams);

    ////        HttpResponseMessage response = await _httpClient.PostAsync(tokenUrl, content, cancellationToken);

    //        if (!response.IsSuccessStatusCode)
    ////        {
    ////            string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
    //            _logger.LogError("Failed to get Keycloak admin token. Status: {StatusCode}, Error: {Error}",
    ////                response.StatusCode, errorContent);
    ////            return Result.Failure<string>(Error.Failure("Keycloak.TokenFailure", "Failed to get admin access token"));
    ////       }

    ////       string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
    ////        JsonElement tokenResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

    ////       string? accessToken = tokenResponse.GetProperty("access_token").GetString();
    ////        return Result.Success(accessToken!);
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        _logger.LogError(ex, "Exception occurred while getting Keycloak admin token");
    ////       return Result.Failure<string>(Error.Failure("Keycloak.Exception", ex.Message));
    ////    }
    ////}

    private async Task<Result<string?>> GetUserIdFromEmail(string email)
    {
        try
        {
            List<UserRepresentation>? users =
                await client.Users
                    .GetAsync(requestConfiguration =>
                    {
                        requestConfiguration.QueryParameters = new UsersRequestBuilderGetQueryParameters
                        {
                            Email = email,
                            Exact = true,
                            Max = 1,
                        };
                    });

            users ??= [];

            return Result.Success(users.FirstOrDefault()?.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user ID from email {Email} in realm", email);

            return Result.Failure<string?>(Error.Failure(
                "Error getting user Id from email",
                ex.Message));
        }
    }
}
