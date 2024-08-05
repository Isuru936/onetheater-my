using Microsoft.EntityFrameworkCore;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Customers;
using OneTheater.Modules.Shows.Infrastructure.Customers;

namespace OneTheater.Modules.Shows.Infrastructure.Database;
public sealed class ShowsDbContext(DbContextOptions<ShowsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Shows);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}
