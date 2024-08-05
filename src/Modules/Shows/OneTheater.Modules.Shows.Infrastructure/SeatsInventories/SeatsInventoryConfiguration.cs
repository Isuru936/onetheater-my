using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OneTheater.Modules.Shows.Domain.SeatsInventories;

namespace OneTheater.Modules.Shows.Infrastructure.SeatsInventories;
internal sealed class SeatsInventoryConfiguration : IEntityTypeConfiguration<SeatsInventory>
{
    public void Configure(EntityTypeBuilder<SeatsInventory> builder)
    {
        builder.HasKey(x => new { x.ShowId, x.SeatId });
        builder.Property(x => x.SeatNumber).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Status).IsRequired();
    }
}
