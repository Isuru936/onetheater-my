namespace OneTheater.Modules.Users.Domain.Abstractions;

public interface IDomainEvent
{
    Guid Id { get; }

    DateTime OccuredOnUtc { get; }
}
