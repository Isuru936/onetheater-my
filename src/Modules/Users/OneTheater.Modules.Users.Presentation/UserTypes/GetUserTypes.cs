using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Common.Presentation.ApiResults;
using OneTheater.Common.Presentation.Endpoints;
using OneTheater.Modules.Users.Application.UserTypes.GetUserTypes;

namespace OneTheater.Modules.Users.Presentation.UserTypes;
internal sealed class GetUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("usertypes", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<UserTypeResponse>> result = await sender.Send(new GetUserTypesQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .WithTags(Tags.UserTypes);
    }
}
