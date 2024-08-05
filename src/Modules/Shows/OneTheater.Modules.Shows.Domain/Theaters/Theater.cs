using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Domain.Movies;

namespace OneTheater.Modules.Shows.Domain.Theaters;
public sealed class Theater : Entity
{
    private Theater()
    { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Result<Theater> Create(string name)
    {
        var movie = new Theater()
        {
            Name = name
        };

        return movie;
    }
}
