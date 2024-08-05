using OneTheater.Modules.Shows.Domain.Customers;

namespace OneTheater.Modules.Shows.Domain.Customers;
public interface ICustomerRepository
{
    void Insert(Customer customer);
}
