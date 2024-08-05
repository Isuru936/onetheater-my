using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Shows.Application.Screens.CreateScreen;

public sealed record CreateScreenCommand(string Name, Guid TheaterId) : ICommand<Guid>;
