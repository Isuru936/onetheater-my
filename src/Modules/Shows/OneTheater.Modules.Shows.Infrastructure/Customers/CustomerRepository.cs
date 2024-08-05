using OneTheater.Modules.Shows.Domain.Customers;
using OneTheater.Modules.Shows.Infrastructure.Database;

namespace OneTheater.Modules.Shows.Infrastructure.Customers;
internal sealed class CustomerRepository(ShowsDbContext context) : ICustomerRepository
{
    public void Insert(Customer customer)
    {
        context.Add(customer);
    }
}
