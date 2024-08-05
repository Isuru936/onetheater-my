namespace OneTheater.Modules.Shows.Domain.SeatsInventories;
public interface ISeatsInventoryRepository
{
    void Insert(List<SeatsInventory> seatsInventory);
}
