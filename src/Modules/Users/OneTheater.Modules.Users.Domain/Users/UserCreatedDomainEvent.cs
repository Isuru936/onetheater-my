using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Users.Domain.Users;

public sealed class UserCreatedDomainEvent(Guid userId, string username) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
    public string Username { get; init; } = username;
}
