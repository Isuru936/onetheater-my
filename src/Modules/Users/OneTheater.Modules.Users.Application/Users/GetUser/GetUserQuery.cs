using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
