namespace OneTheater.Common.Application.EventBus;

public interface IIntegrationEvent
{
    Guid Id { get; }

    DateTime OccurdedOnUtc { get; }
}
