namespace OneTheater.Common.Application.EventBus;

public abstract class IntegrationEvent : IIntegrationEvent
{
    protected IntegrationEvent(Guid id, DateTime occurdedOnUtc)
    {
        Id = id;
        OccurdedOnUtc = occurdedOnUtc;
    }

    public Guid Id { get; init; }

    public DateTime OccurdedOnUtc { get; init; }
}
