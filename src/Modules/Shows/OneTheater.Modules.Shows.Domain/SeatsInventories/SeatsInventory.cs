using System.Collections.Generic;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Domain.Screens;
using OneTheater.Modules.Shows.Domain.Seats;
using OneTheater.Modules.Shows.Domain.Shows;

namespace OneTheater.Modules.Shows.Domain.SeatsInventories;
public sealed class SeatsInventory : Entity
{
    private SeatsInventory()
    { }

    public Guid ShowId { get; private set; }
    public Show Show { get; private set; }
    public Guid ScreenId { get; private set; }
    public Screen Screen { get; private set; }
    public Guid SeatId { get; private set; }
    public string SeatNumber { get; private set; }
    public SeatStatus Status { get; private set; }

    public static Result<List<SeatsInventory>> RegisterSeatsInventoryScreen(Guid showId, List<Seat> seatsArrangement)
    {
        var inventory = new List<SeatsInventory>();

        foreach (Seat seat in seatsArrangement)
        {
            inventory.Add(new SeatsInventory()
            {
                ShowId = showId,
                ScreenId = seat.ScreenId,
                SeatId = seat.Id,
                SeatNumber = seat.Name,
                Status = SeatStatus.Free
            });
        }

        return inventory;
    }
}

public enum SeatStatus
{
    Free,
    Locked,
    Booked
}
