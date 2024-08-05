using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Domain.Seats;
using OneTheater.Modules.Shows.Domain.Theaters;

namespace OneTheater.Modules.Shows.Domain.Screens;
public sealed class Screen : Entity
{
    private Screen()
    { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid TheaterId { get; private set; }
    public Theater Theater { get; private set; }
    public List<Seat> Seats { get; private set; }

    public static Result<Screen> Create(string name, Guid theaterId)
    {
        var screenId = Guid.NewGuid();

        var screen = new Screen()
        {
            Id = screenId,
            Name = name,
            TheaterId = theaterId,
            Seats = Seat.NewScreenSeats(10, 10, screenId)
        };

        ////screen.Raise(new ScreenCreatedDomainEvent(screen.Id, screen.Name, screen.TheaterId));

        return screen;
    }
}
