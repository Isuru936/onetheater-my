using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Users.IntegrationEvents;

namespace OneTheater.Modules.Shows.Presentation.Customers;

internal sealed class GetCustomerContact : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{id}/contact", async (Guid id, IRequestClient<GetUserContactDetailsRequest> client) =>
        {
            Response<UserContactDetailsResponse> response = await client.GetResponse<UserContactDetailsResponse>(new GetUserContactDetailsRequest
            {
                UserId = id
            });

            if (response.Message.Email == null && response.Message.Phone == null && response.Message.Address == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response.Message);
        })
        .WithTags(Tags.Customers);
    }
}


