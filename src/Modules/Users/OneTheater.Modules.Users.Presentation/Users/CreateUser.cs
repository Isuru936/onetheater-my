using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Users.Application.Users.CreateUser;
using OneTheater.Modules.Users.Presentation.ApiResults;

namespace OneTheater.Modules.Users.Presentation.Users;


internal static class CreateUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (Request request, ISender sender) =>
            {
                var command = new CreateUserCommand(request.Id, request.UserName);

                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
            })
            .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
