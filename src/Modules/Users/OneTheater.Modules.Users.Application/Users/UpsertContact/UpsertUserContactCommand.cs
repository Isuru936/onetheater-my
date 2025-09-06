using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Users.Application.Users.UpsertContact;

public sealed record UpsertUserContactCommand(Guid UserId, string Email) : ICommand<Guid>;





