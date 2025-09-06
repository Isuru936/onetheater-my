using Microsoft.EntityFrameworkCore;
using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.Infrastructure.Database;

namespace OneTheater.Modules.Users.Infrastructure.Users;
internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public void Insert(User user)
    {
        context.Add(user);
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task UpdateEmailAsync(Guid userId, string email, CancellationToken cancellationToken = default)
    {
        await context.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(s => s.SetProperty(u => u.Email, email), cancellationToken);
    }
}
