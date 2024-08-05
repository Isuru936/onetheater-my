using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Users.Domain.UserTypes;

public sealed class UserType : Entity
{
    private UserType()
    { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Result<UserType> Create(string name)
    {
        var userType = new UserType
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        userType.Raise(new UserTypeCreatedDomainEvent(userType.Id, userType.Name));

        return userType;
    }
}

public static class UserTypeErrors
{
    public static Error NotFound(Guid userTypeId) =>
    Error.NotFound("UserTypes.NotFound", $"The user type with the identifier {userTypeId} was not found");

    public static readonly Error Name = Error.Problem("UserTypes.Name", "Name is too much lengthy.");
}
