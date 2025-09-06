namespace OneTheater.Modules.Users.IntegrationEvents;

public sealed class UserContactDetailsResponse
{
    public Guid UserId { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? Address { get; init; }
}


