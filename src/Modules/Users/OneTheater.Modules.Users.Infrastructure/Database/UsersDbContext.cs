using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OneTheater.Common.Infrastructure.Outbox;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.Domain.UserTypes;
using OneTheater.Modules.Users.Domain.Contacts;
using OneTheater.Modules.Users.Infrastructure.Users;
using OneTheater.Modules.Users.Infrastructure.UserTypes;
using OneTheater.Modules.Users.Infrastructure.Contacts;

namespace OneTheater.Modules.Users.Infrastructure.Database;
public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<UserType> UserTypes { get; set; }
    internal DbSet<ContactInfo> ContactInfos { get; set; }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ContactInfoConfiguration());
    }
}
