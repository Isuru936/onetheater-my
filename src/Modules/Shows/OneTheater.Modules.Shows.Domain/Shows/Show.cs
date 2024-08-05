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
    public DateTime ShowTime { get; private set; }
    public Guid ScreenId { get; private set; }
    public List<SeatsInventory> SeatsInventories { get; private set; }

    public static Result<Show> Create(Guid movieId, List<Seat> seatsOftheScreen)
    {
        var showId = Guid.NewGuid();

        var user = new Show
        {
            Id = showId,
            MovieId = movieId,
            SeatsInventories = SeatsInventory.RegisterSeatsInventoryScreen(showId, seatsOftheScreen).Value
        };

        ////user.Raise(new ShowCreatedDomainEvent(user.Id));

        return user;
    }
}
