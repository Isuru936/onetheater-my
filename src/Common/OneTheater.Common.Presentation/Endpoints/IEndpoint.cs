using Microsoft.AspNetCore.Routing;

namespace OneTheater.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
