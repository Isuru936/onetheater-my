using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OneTheater.Modules.Shows.Domain.Shows;

namespace OneTheater.Modules.Shows.Infrastructure.Shows;
internal sealed class ShowConfiguration : IEntityTypeConfiguration<Show>
{
    public void Configure(EntityTypeBuilder<Show> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ShowTime).IsRequired();
        builder.Property(x => x.MovieId).IsRequired();
        builder.Property(x => x.ScreenId).IsRequired();
    }
}
