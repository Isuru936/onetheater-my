using OneTheater.Modules.Users.Domain.UserTypes;
using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.Infrastructure.Database;

namespace OneTheater.Modules.Users.Infrastructure.UserTypes;
internal sealed class UserTypeRepository(UsersDbContext context) : IUserTypeRepository
{
    public void Insert(UserType userType)
    {
        context.Add(userType);
    }
}
