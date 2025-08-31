using MediatR;
using OneTheater.Common.Application.Abstrations.Exceptions;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Application.EventBus;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Users.Application.Users.GetUser;
using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.IntegrationEvents;

namespace OneTheater.Modules.Users.Application.Users.CreateUser;
internal sealed class UserCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : IDomainEventHandler<UserCreatedDomainEvent>
{
    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // Future Reference - This is how to publish an event sync

        ////var request = new CustomerCreateRequest()
        ////{
        ////    UserId = notification.UserId,
        ////    FirstName = notification.FirstName,
        ////    Email = notification.Email,
        ////    LastName = notification.LastName
        ////};

        ////await customersApi.PostAsync(request, cancellationToken);

        Result<UserResponse> result = await sender.Send(new GetUserQuery(notification.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new OneTheaterException("User not found");
        }

        await eventBus.PublishAsync(new UserCreatedIntegrationEvent(
            notification.Id,
            notification.OccurredOnUtc,
            result.Value.Id,
            result.Value.FirstName,
            result.Value.LastName,
            result.Value.Email
        ), cancellationToken);
    }
}
