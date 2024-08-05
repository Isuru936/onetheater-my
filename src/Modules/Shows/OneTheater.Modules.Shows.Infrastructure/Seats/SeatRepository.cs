using OneTheater.Modules.Shows.Domain.Seats;
using OneTheater.Modules.Shows.Infrastructure.Database;

namespace OneTheater.Modules.Shows.Infrastructure.Seats;
internal sealed class SeatRepository(ShowsDbContext context) : ISeatRepository
{
    public void Insert(List<Seat> seats)
    {
        context.AddRange(seats);
    }
}
