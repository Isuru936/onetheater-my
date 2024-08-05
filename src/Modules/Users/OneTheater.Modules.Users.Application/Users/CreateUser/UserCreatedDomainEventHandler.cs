using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Modules.Users.Domain.Users;

namespace OneTheater.Modules.Users.Application.Users.CreateUser;
internal sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
{
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
