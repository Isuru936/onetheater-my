using OneTheater.Modules.Users.Domain.UserTypes;

namespace OneTheater.Modules.Users.Domain.Users;
public  interface IUserTypeRepository
{
    void Insert(UserType userType);
}
