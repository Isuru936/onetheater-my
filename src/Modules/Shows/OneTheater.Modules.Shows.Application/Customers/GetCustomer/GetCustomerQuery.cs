using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Shows.Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(Guid CustomerId) : IQuery<CustomerResponse>;
