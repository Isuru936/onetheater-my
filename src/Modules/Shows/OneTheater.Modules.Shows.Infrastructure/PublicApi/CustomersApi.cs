using MediatR;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Application.Customers.CreateCustomer;
using OneTheater.Modules.Shows.PublicApi;

namespace OneTheater.Modules.Shows.Infrastructure.PublicApi;
internal sealed class CustomersApi(ISender sender) : ICustomersApi
{
    public async Task<Guid?> PostAsync(CustomerCreateRequest request, CancellationToken cancellationToken = default)
    {
        Result<Guid> result = await sender.Send(new CreateCustomerCommand(request.UserId, request.FirstName, request.LastName, request.Email), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        return result.Value;
    }
}
