using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Shows.Application.Screens.CreateScreen;

namespace OneTheater.Modules.Shows.Presentation.Screens;

internal sealed class CreateScreen : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("screens", async (Request request, ISender sender) =>
        {
            var command = new CreateScreenCommand(request.Name, request.TheaterId);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .WithTags(Tags.Screens);
    }

    internal sealed class Request
    {
        public string Name { get; set; }

        public Guid TheaterId { get; set; }
    }
}
