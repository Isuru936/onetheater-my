using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Users.Domain.UserTypes;

public sealed class UserTypeCreatedDomainEvent(Guid userTypeId, string userTypeName) : DomainEvent
{
    public Guid UserTypeId { get; init; } = userTypeId;
    public string UserTypeName { get; init; } = userTypeName;
}
