using System.Data.Common;
using Dapper;
using OneTheater.Common.Application.Abstrations.Data;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Shows.Application.Customers.GetCustomer;

internal sealed class GetCustomerQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetCustomerQuery, CustomerResponse>
{
    public async Task<Result<CustomerResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection conneciton = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            $"""
                SELECT 
                    id AS {nameof(CustomerResponse.Id)},
                    first_name AS {nameof(CustomerResponse.FirstName)}
                    last_name AS {nameof(CustomerResponse.LastName)}
                    email AS {nameof(CustomerResponse.Email)}
                FROM shows.customers WHERE Id = @CustomerId
            """;

        CustomerResponse? customer = await conneciton.QuerySingleOrDefaultAsync<CustomerResponse>(sql, request);

        return customer;
    }
}
