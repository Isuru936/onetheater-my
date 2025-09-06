using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Users.Application.Users.Contacts;

public sealed record UpsertContactInfoCommand(Guid UserId, string Email, string? Phone, string? Address) : ICommand<Guid>;



