using MediatR;
using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Users.Application.Users.CreateUser;

public sealed record CreateUserCommand(string Username) : ICommand<Guid>;
