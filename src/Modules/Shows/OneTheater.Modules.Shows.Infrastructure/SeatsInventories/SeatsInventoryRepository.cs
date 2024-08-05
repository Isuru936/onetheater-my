using OneTheater.Modules.Shows.Domain.SeatsInventories;
using OneTheater.Modules.Shows.Infrastructure.Database;

namespace OneTheater.Modules.Shows.Infrastructure.SeatsInventories;
internal sealed class SeatsInventoryRepository(ShowsDbContext context) : ISeatsInventoryRepository
{
    public void Insert(List<SeatsInventory> seatsInventory)
    {
        context.AddRange(seatsInventory);
    }
}
