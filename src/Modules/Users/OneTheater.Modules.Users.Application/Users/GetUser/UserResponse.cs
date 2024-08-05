namespace OneTheater.Modules.Users.Application.Users.GetUser;

public sealed record UserResponse(
    Guid Id, string Username, string FirstName, string LastName, string Email);
