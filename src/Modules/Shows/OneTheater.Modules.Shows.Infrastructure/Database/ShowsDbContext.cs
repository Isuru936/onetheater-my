using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Customers;
using OneTheater.Modules.Shows.Domain.Movies;
using OneTheater.Modules.Shows.Domain.Screens;
using OneTheater.Modules.Shows.Domain.Seats;
using OneTheater.Modules.Shows.Domain.SeatsInventories;
using OneTheater.Modules.Shows.Domain.Shows;
using OneTheater.Modules.Shows.Domain.Theaters;
using OneTheater.Modules.Shows.Infrastructure.Customers;
using OneTheater.Modules.Shows.Infrastructure.Movies;
using OneTheater.Modules.Shows.Infrastructure.Screens;
using OneTheater.Modules.Shows.Infrastructure.Seats;
using OneTheater.Modules.Shows.Infrastructure.SeatsInventories;
using OneTheater.Modules.Shows.Infrastructure.Shows;
using OneTheater.Modules.Shows.Infrastructure.Theaters;

namespace OneTheater.Modules.Shows.Infrastructure.Database;
public sealed class ShowsDbContext(DbContextOptions<ShowsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Customer> Customers { get; set; }
    internal DbSet<Movie> Movies { get; set; }
    internal DbSet<Theater> Theaters { get; set; }
    internal DbSet<Screen> Screens { get; set; }
    internal DbSet<Seat> Seats { get; set; }
    internal DbSet<SeatsInventory> SeatsInventories { get; set; }
    internal DbSet<Show> Shows { get; set; }

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
        modelBuilder.HasDefaultSchema(Schemas.Shows);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new TheaterConfiguration());
        modelBuilder.ApplyConfiguration(new ScreenConfiguration());
        modelBuilder.ApplyConfiguration(new SeatConfiguration());
        modelBuilder.ApplyConfiguration(new SeatsInventoryConfiguration());
        modelBuilder.ApplyConfiguration(new ShowConfiguration());

    }
}
