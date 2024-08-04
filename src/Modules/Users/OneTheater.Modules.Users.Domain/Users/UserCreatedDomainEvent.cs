using OneTheater.Modules.Users.Domain.Abstractions;

namespace OneTheater.Modules.Users.Domain.Users;

public sealed class UserCreatedDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
