using Microsoft.EntityFrameworkCore;
using OneTheater.Modules.Shows.Domain.Seats;
using OneTheater.Modules.Shows.Infrastructure.Database;

namespace OneTheater.Modules.Shows.Infrastructure.Seats;
internal sealed class SeatRepository(ShowsDbContext context) : ISeatRepository
{
    public async Task<List<Seat>> GetByScreenId(Guid screenId)
    {
        return await context.Seats.Where(c => c.ScreenId == screenId).ToListAsync();
    }

    public void Insert(List<Seat> seats)
    {
        context.AddRange(seats);
    }
}
