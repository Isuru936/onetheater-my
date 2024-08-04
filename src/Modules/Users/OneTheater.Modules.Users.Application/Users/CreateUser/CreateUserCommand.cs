using MediatR;
using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Users.Application.Users.CreateUser;

public sealed record CreateUserCommand(Guid Id, string Username) : ICommand<Guid>;
