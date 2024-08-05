using System.Security.Cryptography.X509Certificates;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Domain.Screens;

namespace OneTheater.Modules.Shows.Domain.Seats;
public sealed class Seat : Entity
{
    private Seat()
    { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ScreenId { get; private set; }
    public Screen Screen { get; private set; }

    internal static List<Seat> NewScreenSeats(int lines, int columns, Guid screenId)
    {
        var seats = new List<Seat>();

        for (int i = 1; i <= lines; i++)
        {
            for (int j = 1; j <= columns; j++)
            {
                seats.Add(new Seat()
                {
                    Id = Guid.NewGuid(),
                    Name = $"{i}-{j}",
                    ScreenId = screenId
                });
            }
        }

        return seats;
    }
}
