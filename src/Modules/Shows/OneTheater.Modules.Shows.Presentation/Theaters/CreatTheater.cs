using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Shows.Application.Theaters.CreateTheater;

namespace OneTheater.Modules.Shows.Presentation.Theaters;

internal sealed class CreatTheater : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("theaters", async (Request request, ISender sender) =>
        {
            var command = new CreateTheaterCommand(request.Name);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .WithTags(Tags.Theater);
    }

    internal sealed class Request
    {
        public string Name { get; set; }
    }
}
