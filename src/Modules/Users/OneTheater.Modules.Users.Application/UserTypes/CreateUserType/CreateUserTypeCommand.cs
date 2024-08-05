using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Users.Application.UserTypes.CreateUserType;
public sealed record CreateUserTypeCommand(string Name) : ICommand<Guid>;
