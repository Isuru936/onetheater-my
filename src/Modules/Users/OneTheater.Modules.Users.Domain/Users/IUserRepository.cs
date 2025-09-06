namespace OneTheater.Modules.Users.Domain.Users;
public interface IUserRepository
{
    void Insert(User user);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateEmailAsync(Guid userId, string email, CancellationToken cancellationToken = default);
}
