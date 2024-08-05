using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Modules.Shows.Domain.Customers;

namespace OneTheater.Modules.Shows.Application.Customers.CreateCustomer;
internal sealed class CustomerCreatedDomainEventHandler : IDomainEventHandler<CustomerCreatedDomainEvent>
{
    public Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
