using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTheater.Common.Application.EventBus;

namespace OneTheater.Modules.Users.IntegrationEvents;
public class KeycloakCreateUserIntegrationEvent : IntegrationEvent
{
    public KeycloakCreateUserIntegrationEvent(Guid id, DateTime occurdedOnUtc, Guid userId, string userName, string firstName, string lastName, string email) : base(id, occurdedOnUtc)
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
