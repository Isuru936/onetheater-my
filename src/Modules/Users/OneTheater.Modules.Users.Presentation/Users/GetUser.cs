using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Modules.Users.Application.Users.GetUser;
using OneTheater.Modules.Users.Domain.Abstractions;
using OneTheater.Modules.Users.Presentation.ApiResults;

namespace OneTheater.Modules.Users.Presentation.Users;

internal static partial class GetUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id}", async (Guid id, ISender sender) =>
            {
                Result<UserResponse> result = await sender.Send(new GetUserQuery(id));

                return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}
