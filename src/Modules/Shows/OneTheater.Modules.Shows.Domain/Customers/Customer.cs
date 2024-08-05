using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Shows.Domain.Customers;

public sealed class Customer : Entity
{
    private Customer()
    { }

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public static Result<Customer> Create(string firstName, string lastName, string email)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        customer.Raise(new CustomerCreatedDomainEvent(customer.Id, customer.FirstName, customer.LastName, customer.Email));

        return customer;
    }
}

public static class CustomerErrors
{
    public static Error NotFound(Guid cusId) =>
    Error.NotFound("Customer.NotFound", $"The customer with the identifier {cusId} was not found");
}
