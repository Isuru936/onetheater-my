using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OneTheater.Modules.Shows.Domain.Seats;

namespace OneTheater.Modules.Shows.Infrastructure.Seats;
internal sealed class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(10);
        builder.Property(x => x.ScreenId).IsRequired();
    }
}
