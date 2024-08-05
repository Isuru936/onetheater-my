using OneTheater.Modules.Shows.Domain.Screens;
using OneTheater.Modules.Shows.Infrastructure.Database;

namespace OneTheater.Modules.Shows.Infrastructure.Screens;
internal sealed class ScreenRepository(ShowsDbContext context) : IScreenRepository
{
    public void Insert(Screen screen)
    {
        context.Add(screen);
    }
}
