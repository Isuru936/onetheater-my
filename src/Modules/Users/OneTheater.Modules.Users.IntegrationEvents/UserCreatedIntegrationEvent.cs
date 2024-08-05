using OneTheater.Common.Application.EventBus;

namespace OneTheater.Modules.Users.IntegrationEvernt;

public sealed class UserCreatedIntegrationEvent : IntegrationEvent
{
    public UserCreatedIntegrationEvent(Guid id, DateTime occurdedOnUtc, Guid userId, string lastName, string firstName, string email) : base(id, occurdedOnUtc)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public Guid UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}
