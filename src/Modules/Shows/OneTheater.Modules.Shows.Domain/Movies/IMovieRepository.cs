using OneTheater.Modules.Shows.Domain.Customers;

namespace OneTheater.Modules.Shows.Domain.Movies;

public interface IMovieRepository
{
    void Insert(Movie movie);
}
