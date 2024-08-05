using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Shows.Application.Shows.CreateShow;

public sealed record CreateShowCommand(Guid MovieId, Guid ScreenId, DateTime ShowTime) : ICommand<Guid>;
