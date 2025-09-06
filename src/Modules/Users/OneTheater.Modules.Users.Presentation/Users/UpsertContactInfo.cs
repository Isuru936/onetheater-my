using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Users.Application.Users.Contacts;

namespace OneTheater.Modules.Users.Presentation.Users;

internal sealed class UpsertContactInfo : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/{id}/contact-info", async (Guid id, Request request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new UpsertContactInfoCommand(id, request.Email, request.Phone, request.Address));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}



