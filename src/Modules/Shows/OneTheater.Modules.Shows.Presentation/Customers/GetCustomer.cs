using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Shows.Application.Customers.GetCustomer;
using OneTheater.Modules.Shows.Presentation;

namespace OneTheater.Modules.Shows.Presentation.Customers;

internal sealed class GetUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{id}", async (Guid id, ISender sender) =>
            {
                Result<CustomerResponse> result = await sender.Send(new GetCustomerQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Customers);
    }
}
