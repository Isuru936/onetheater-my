using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Modules.Shows.PublicApi;
using OneTheater.Modules.Users.Domain.Users;

namespace OneTheater.Modules.Users.Application.Users.CreateUser;
internal sealed class UserCreatedDomainEventHandler(ICustomersApi customersApi)
    : IDomainEventHandler<UserCreatedDomainEvent>
{
    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var request = new CustomerCreateRequest()
        {
            UserId = notification.UserId,
            FirstName = notification.FirstName,
            Email = notification.Email,
            LastName = notification.LastName
        };

        await customersApi.PostAsync(request, cancellationToken);
    }
}
