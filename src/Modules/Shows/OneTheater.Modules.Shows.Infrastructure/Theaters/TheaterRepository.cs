using OneTheater.Modules.Shows.Domain.Theaters;
using OneTheater.Modules.Shows.Infrastructure.Database;

namespace OneTheater.Modules.Shows.Infrastructure.Theaters;
internal sealed class TheaterRepository(ShowsDbContext context) : ITheaterRepository
{
    public void Insert(Theater theater)
    {
        context.Add(theater);
    }
}
