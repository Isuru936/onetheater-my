using MediatR;

namespace OneTheater.Common.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccuredOnUtc { get; }
}
