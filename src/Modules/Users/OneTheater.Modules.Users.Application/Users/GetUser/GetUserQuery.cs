using OneTheater.Modules.Users.Application.Abstractions.Messaging;

namespace OneTheater.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
