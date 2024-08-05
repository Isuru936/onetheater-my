using OneTheater.Modules.Shows.Domain.Movies;
using OneTheater.Modules.Shows.Infrastructure.Database;

namespace OneTheater.Modules.Shows.Infrastructure.Movies;

internal sealed class MovieRepository(ShowsDbContext context) : IMovieRepository
{
    public void Insert(Movie movie)
    {
        context.Add(movie);
    }
}
