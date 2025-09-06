using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Users.Domain.Users.Events;
public sealed class KeycloakCreateUserDomainEvent : DomainEvent
{
    public KeycloakCreateUserDomainEvent(Guid userId, string userName, string firstName, string lastName, string email)
    {
        UserId = userId;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public Guid UserId { get; }
    public string UserName { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
}
