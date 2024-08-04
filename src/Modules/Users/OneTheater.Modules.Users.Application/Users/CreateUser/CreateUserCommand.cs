using MediatR;
using OneTheater.Modules.Users.Application.Abstractions.Messaging;

namespace OneTheater.Modules.Users.Application.Users.CreateUser;

public sealed record CreateUserCommand(Guid Id, string Username) : ICommand<Guid>;
