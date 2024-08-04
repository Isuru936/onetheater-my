using Microsoft.AspNetCore.Routing;

namespace OneTheater.Modules.Users.Presentation.Users;
public static class UsersEndpoints
{
    public static void MapEndPoints(IEndpointRouteBuilder app)
    {
        CreateUser.MapEndpoint(app);
        GetUser.MapEndpoint(app);
    }
}
