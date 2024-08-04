namespace OneTheater.Common.Domain.Abstractions;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccuredOnUtc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occuredOnUtc)
    {
        Id = id;
        OccuredOnUtc = occuredOnUtc;
    }

    public Guid Id { get; init; }

    public DateTime OccuredOnUtc { get; init; }
}
