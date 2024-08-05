using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Customers;

namespace OneTheater.Modules.Shows.Application.Customers.CreateCustomer;

internal sealed class CreateUserCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Result<Customer> result = Customer.Create(request.FirstName, request.LastName, request.Email);

        if (result.IsSuccess)
        {
            repository.Insert(result.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
