using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Common.Application.Abstractions;

public interface IKeycloakService
{
    Task<Result<string>> CreateUserAsync(CreateKeycloakUserRequest request, CancellationToken cancellationToken = default);
}

public sealed record CreateKeycloakUserRequest(
    string Username,
    string Email,
    string FirstName,
    string LastName,
    bool Enabled = true,
    bool EmailVerified = false);

public sealed record UpdateKeycloakUserRequest(
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    bool? Enabled = null,
    bool? EmailVerified = null);


