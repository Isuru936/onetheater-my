using MediatR;
using OneTheater.Common.Application.Abstractions;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Application.EventBus;
using OneTheater.Modules.Users.Domain.Users.Events;
using OneTheater.Modules.Users.IntegrationEvents;

namespace OneTheater.Modules.Users.Application.Users.Events;
internal sealed class KeycloakCreateUserDomainEventHandler(IEventBus eventBus, IKeycloakService keycloak) : IDomainEventHandler<KeycloakCreateUserDomainEvent>
{
    public async Task Handle(KeycloakCreateUserDomainEvent notification, CancellationToken cancellationToken)
    {
        await keycloak.CreateUserAsync(new CreateKeycloakUserRequest(notification.UserName, notification.Email, notification.FirstName, notification.LastName, true, true), cancellationToken);

        await eventBus.PublishAsync(new UserCreatedIntegrationEvent(
            notification.Id,
            notification.OccurredOnUtc,
            notification.UserId,
            notification.FirstName,
            notification.LastName,
            notification.Email
        ), cancellationToken);
    }
}
