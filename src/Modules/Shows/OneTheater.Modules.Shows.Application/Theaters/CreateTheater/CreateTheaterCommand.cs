using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Shows.Application.Theaters.CreateTheater;

public sealed record CreateTheaterCommand(string Name) : ICommand<Guid>;
