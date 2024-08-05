using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Shows.Domain.Customers;

public sealed class CustomerCreatedDomainEvent(Guid customerId, string firstName, string lastName, string email) : DomainEvent
{
    public Guid CustomerId { get; init; } = customerId;
    public string Email { get; init; } = email;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
}
