using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneTheater.Modules.Users.Domain.Contacts;

namespace OneTheater.Modules.Users.Infrastructure.Contacts;
internal sealed class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
{
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Phone).HasMaxLength(30);
        builder.Property(x => x.Address).HasMaxLength(2000);
        builder.HasIndex(x => x.UserId).IsUnique();
    }
}





