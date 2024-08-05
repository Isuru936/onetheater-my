using Microsoft.EntityFrameworkCore;
using OneTheater.Common.Infrastructure.Outbox;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.Domain.UserTypes;
using OneTheater.Modules.Users.Infrastructure.Users;
using OneTheater.Modules.Users.Infrastructure.UserTypes;

namespace OneTheater.Modules.Users.Infrastructure.Database;
public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<UserType> UserTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
    }
}
