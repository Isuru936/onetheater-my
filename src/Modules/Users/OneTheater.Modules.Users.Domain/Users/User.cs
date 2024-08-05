using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Users.Domain.Users;

public sealed class User : Entity
{
    private User()
    { }

    public Guid Id { get; private set; }
    public string Username { get; private set; }

    public static Result<User> Create(string username)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username
        };

        if (username == "admin")
        {

            return Result.Failure<User>(UserErrors.AdminUserName);
        }

        user.Raise(new UserCreatedDomainEvent(user.Id, user.Username));

        return user;
    }
}

public static class UserErrors
{
    public static Error NotFound(Guid userId) =>
    Error.NotFound("Users.NotFound", $"The user with the identifier {userId} was not found");

    public static readonly Error AdminUserName = Error.Problem("Users.Username", "Admin username is not allowed.");
}
