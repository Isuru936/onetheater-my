using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.Infrastructure.Database;

namespace OneTheater.Modules.Users.Infrastructure.Users;
internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public void Insert(User user)
    {
        context.Add(user);
    }
}
