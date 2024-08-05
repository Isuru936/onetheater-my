using System.ComponentModel;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Domain.Customers;

namespace OneTheater.Modules.Shows.Domain.Movies;
public sealed class Movie : Entity
{
    private Movie()
    { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Result<Movie> Create(string name)
    {
        var movie = new Movie()
        {
            Name = name
        };

        ////movie.Raise(new MovieCreatedDomainEvent(movie.Id, movie.Name));

        return movie;
    }
}
