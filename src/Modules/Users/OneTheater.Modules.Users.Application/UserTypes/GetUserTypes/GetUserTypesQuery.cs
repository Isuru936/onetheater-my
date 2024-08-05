using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Users.Application.UserTypes.GetUserTypes;
public sealed record GetUserTypesQuery : IQuery<IReadOnlyCollection<UserTypeResponse>>;
