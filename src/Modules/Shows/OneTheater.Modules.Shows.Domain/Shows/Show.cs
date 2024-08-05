using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Domain.Seats;
using OneTheater.Modules.Shows.Domain.SeatsInventories;

namespace OneTheater.Modules.Shows.Domain.Shows;
public sealed class Show : Entity
{
    private Show()
    { }

    public Guid Id { get; private set; }

    public Guid MovieId { get; private set; }
    public DateTimeOffset ShowTime { get; private set; }
    public Guid ScreenId { get; private set; }
    public List<SeatsInventory> SeatsInventories { get; private set; }

    public static Result<Show> Create(Guid movieId, Guid screenId, DateTimeOffset showTime, List<Seat> seatsOftheScreen)
    {
        var showId = Guid.NewGuid();

        var user = new Show
        {
            Id = showId,
            MovieId = movieId,
            ScreenId = screenId,
            ShowTime = showTime,
            SeatsInventories = SeatsInventory.RegisterSeatsInventoryScreen(showId, seatsOftheScreen).Value
        };

        return user;
    }
}
