using OneTheater.Modules.Shows.Domain.Shows;
using OneTheater.Modules.Shows.Infrastructure.Database;

namespace OneTheater.Modules.Shows.Infrastructure.Shows;

internal sealed class ShowRepository(ShowsDbContext context) : IShowRepository
{
    public void Insert(Show show)
    {
        context.Add(show);
    }
}
