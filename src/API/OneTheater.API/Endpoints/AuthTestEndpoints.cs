using Microsoft.AspNetCore.Authorization;
using OneTheater.Common.Presentation.Endpoints;
using System.Security.Claims;
using System.Text.Json;

namespace OneTheater.API.Endpoints;

internal sealed class AuthTestEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder authGroup = app.MapGroup("auth-test")
            .WithTags("Authentication Tests")
            .WithOpenApi();

        // Public endpoint
        authGroup.MapGet("public", () => new
        {
            Message = "This is a public endpoint accessible to everyone",
            Timestamp = DateTime.UtcNow,
            Status = "success"
        })
        .WithName("PublicEndpoint")
        .WithSummary("Public endpoint - no authentication required")
        .AllowAnonymous();

        // Any authenticated user
        authGroup.MapGet("authenticated", [Authorize] (ClaimsPrincipal user) => new
        {
            Message = "This endpoint requires authentication",
            Username = user.Identity?.Name ?? "Unknown",
            UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? user.FindFirst("sub")?.Value,
            IsAuthenticated = user.Identity?.IsAuthenticated ?? false,
            Timestamp = DateTime.UtcNow,
            Status = "success"
        })
        .WithName("AuthenticatedEndpoint")
        .WithSummary("Authenticated endpoint - requires valid JWT token")
        .RequireAuthorization();

        // Admin only (uses Keycloak realm role "admin")
        authGroup.MapGet("admin-only", [Authorize(Policy = "AdminOnly")] (ClaimsPrincipal user) => 
        {
            // Parse realm roles from the realm_access claim
            Claim? realmAccessClaim = user.FindFirst("realm_access");
            var realmRoles = new List<string>();
            
            if (realmAccessClaim != null)
            {
                try
                {
                    JsonElement realmAccessJson = JsonSerializer.Deserialize<JsonElement>(realmAccessClaim.Value);
                    if (realmAccessJson.TryGetProperty("roles", out JsonElement rolesElement))
                    {
                        realmRoles = rolesElement.EnumerateArray().Select(r => r.GetString() ?? "").ToList();
                    }
                }
                catch (JsonException)
                {
                    // If JSON parsing fails, leave realmRoles empty
                }
            }

            return new
            {
                Message = "This endpoint requires the 'admin' realm role",
                Username = user.Identity?.Name ?? "Unknown",
                UserId = user.FindFirst("sub")?.Value,
                RealmRoles = realmRoles,
                Timestamp = DateTime.UtcNow,
                Status = "success"
            };
        })
        .WithName("AdminOnlyEndpoint")
        .WithSummary("Admin only endpoint - requires Keycloak 'admin' role")
        .RequireAuthorization("AdminOnly");

        // Client or Admin (uses Keycloak realm roles "client" or "admin")
        authGroup.MapGet("client-or-admin", [Authorize(Policy = "ClientOrAdmin")] (ClaimsPrincipal user) => 
        {
            // Parse realm roles from the realm_access claim
            Claim? realmAccessClaim = user.FindFirst("realm_access");
            var realmRoles = new List<string>();
            
            if (realmAccessClaim != null)
            {
                try
                {
                    JsonElement realmAccessJson = JsonSerializer.Deserialize<JsonElement>(realmAccessClaim.Value);
                    if (realmAccessJson.TryGetProperty("roles", out JsonElement rolesElement))
                    {
                        realmRoles = rolesElement.EnumerateArray().Select(r => r.GetString() ?? "").ToList();
                    }
                }
                catch (JsonException)
                {
                    // If JSON parsing fails, leave realmRoles empty
                }
            }

            return new
            {
                Message = "This endpoint requires either 'client' or 'admin' role",
                Username = user.Identity?.Name ?? "Unknown",
                UserId = user.FindFirst("sub")?.Value,
                RealmRoles = realmRoles,
                Timestamp = DateTime.UtcNow,
                Status = "success"
            };
        })
        .WithName("ClientOrAdminEndpoint")
        .WithSummary("Client or Admin endpoint - requires 'client' or 'admin' Keycloak role")
        .RequireAuthorization("ClientOrAdmin");

        // User info endpoint
        authGroup.MapGet("user-info", [Authorize] (ClaimsPrincipal user, HttpContext context) => 
        {
            // Parse realm roles from the realm_access claim
            Claim? realmAccessClaim = user.FindFirst("realm_access");
            var realmRoles = new List<string>();
            
            if (realmAccessClaim != null)
            {
                try
                {
                    JsonElement realmAccessJson = JsonSerializer.Deserialize<JsonElement>(realmAccessClaim.Value);
                    if (realmAccessJson.TryGetProperty("roles", out JsonElement rolesElement))
                    {
                        realmRoles = rolesElement.EnumerateArray().Select(r => r.GetString() ?? "").ToList();
                    }
                }
                catch (JsonException)
                {
                    // If JSON parsing fails, leave realmRoles empty
                }
            }

            return new
            {
                Message = "User information and claims",
                UserInfo = new
                {
                    Username = user.Identity?.Name ?? "Unknown",
                    UserId = user.FindFirst("sub")?.Value,
                    Email = user.FindFirst("email")?.Value,
                    PreferredUsername = user.FindFirst("preferred_username")?.Value,
                    GivenName = user.FindFirst("given_name")?.Value,
                    FamilyName = user.FindFirst("family_name")?.Value,
                    IsAuthenticated = user.Identity?.IsAuthenticated ?? false
                },
                RealmRoles = realmRoles,
                AllClaims = user.Claims.Select(c => new { c.Type, c.Value }).ToList(),
                TokenInfo = new
                {
                    AuthorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault(),
                    HasToken = !string.IsNullOrEmpty(context.Request.Headers["Authorization"].FirstOrDefault())
                },
                Timestamp = DateTime.UtcNow,
                Status = "success"
            };
        })
        .WithName("UserInfoEndpoint")
        .WithSummary("User info endpoint - returns detailed user information and Keycloak claims")
        .RequireAuthorization();

        // Token validation endpoint
        authGroup.MapGet("validate-token", [Authorize] (ClaimsPrincipal user, HttpContext context) =>
        {
            string? tokenHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            string? token = tokenHeader?.Replace("Bearer ", "");

            return new
            {
                Message = "Token is valid",
                TokenValidation = new
                {
                    IsValid = true,
                    TokenLength = token?.Length ?? 0,
                    HasBearer = tokenHeader?.StartsWith("Bearer ", StringComparison.Ordinal) ?? false,
                    ExpirationClaim = user.FindFirst("exp")?.Value,
                    IssuedAtClaim = user.FindFirst("iat")?.Value,
                    IssuerClaim = user.FindFirst("iss")?.Value,
                    AudienceClaim = user.FindFirst("aud")?.Value
                },
                Timestamp = DateTime.UtcNow,
                Status = "success"
            };
        })
        .WithName("ValidateTokenEndpoint")
        .WithSummary("Token validation endpoint - validates JWT token and claims from Keycloak")
        .RequireAuthorization();
    }
}
