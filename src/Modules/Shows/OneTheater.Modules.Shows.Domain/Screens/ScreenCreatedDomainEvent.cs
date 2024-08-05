using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Shows.Domain.Screens;

public sealed class ScreenCreatedDomainEvent(Guid id, string name, Guid theaterId) : DomainEvent
{
    public Guid MovieId { get; init; } = id;
    public string Name { get; init; } = name;
    public Guid TheaterId { get; init; } = theaterId;
}
