using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Users.Application.Users.GetUser;
using OneTheater.Modules.Users.Application.Users.GetUsers;

namespace OneTheater.Modules.Users.Presentation.Users;
internal sealed class GetUsers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (ISender sender) =>
        {
            Result<List<UserResponse>> result = await sender.Send(new GetUsersQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .WithTags(Tags.Users);
    }
}
