using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Users.Application.UserTypes.CreateUserType;

namespace OneTheater.Modules.Users.Presentation.UserTypes;
internal sealed class CreateUserType : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("usertypes", async (Request request, ISender sender) =>
            {
                var command = new CreateUserTypeCommand(request.Name);

                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.UserTypes);
    }

    internal sealed class Request
    {
        public string Name { get; set; }
    }
}
