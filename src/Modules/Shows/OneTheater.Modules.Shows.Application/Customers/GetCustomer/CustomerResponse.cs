namespace OneTheater.Modules.Shows.Application.Customers.GetCustomer;

public sealed record CustomerResponse(
    Guid Id, string FirstName, string LastName, string Email);
