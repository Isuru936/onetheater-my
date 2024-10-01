using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Modules.Users.Application.Users.GetUser;

namespace OneTheater.Modules.Users.Application.Users.GetUsers;
public sealed record GetUsersQuery() : IQuery<List<UserResponse>>;
