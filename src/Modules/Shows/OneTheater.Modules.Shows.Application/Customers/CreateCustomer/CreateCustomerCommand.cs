using MediatR;
using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Shows.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(string Email) : ICommand<Guid>;
