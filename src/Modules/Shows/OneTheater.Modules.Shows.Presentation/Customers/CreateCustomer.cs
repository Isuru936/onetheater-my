using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Shows.Application.Customers.CreateCustomer;

namespace OneTheater.Modules.Shows.Presentation.Customers;


internal sealed class CreateCustomer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers", async (Request request, ISender sender) =>
            {
                var command = new CreateCustomerCommand(request.FirstName, request.LastName, request.Email);

                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Customers);
    }

    internal sealed class Request
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
