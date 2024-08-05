
namespace OneTheater.Modules.Shows.Domain.Seats;
public interface ISeatRepository
{
    void Insert(List<Seat> seats);

    Task<List<Seat>> GetByScreenId(Guid screenId);
}
