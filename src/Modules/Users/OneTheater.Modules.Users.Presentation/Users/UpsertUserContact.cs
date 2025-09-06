using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Users.Application.Users.UpsertContact;

namespace OneTheater.Modules.Users.Presentation.Users;

internal sealed class UpsertUserContact : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/{id}/contact", async (Guid id, Request request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new UpsertUserContactCommand(id, request.Email));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string Email { get; set; }
    }
}





