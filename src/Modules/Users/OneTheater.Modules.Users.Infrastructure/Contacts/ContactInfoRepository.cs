using Microsoft.EntityFrameworkCore;
using OneTheater.Modules.Users.Domain.Contacts;
using OneTheater.Modules.Users.Infrastructure.Database;

namespace OneTheater.Modules.Users.Infrastructure.Contacts;
internal sealed class ContactInfoRepository(UsersDbContext context) : IContactInfoRepository
{
    public async Task<ContactInfo?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Set<ContactInfo>().FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }

    public void Insert(ContactInfo entity)
    {
        context.Add(entity);
    }
}





