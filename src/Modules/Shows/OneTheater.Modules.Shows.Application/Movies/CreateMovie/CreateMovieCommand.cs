using OneTheater.Common.Application.Abstrations.Messaging;

namespace OneTheater.Modules.Shows.Application.Movies.CreateMovie;

public sealed record CreateMovieCommand(string Name) : ICommand<Guid>;
