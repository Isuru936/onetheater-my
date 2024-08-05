using System.Globalization;
using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Shows.Domain.Movies;
public sealed class MovieCreatedDomainEvent(Guid id, string name) : DomainEvent
{
    public Guid MovieId { get; init; } = id;
    public string Name { get; init; } = name;
}
