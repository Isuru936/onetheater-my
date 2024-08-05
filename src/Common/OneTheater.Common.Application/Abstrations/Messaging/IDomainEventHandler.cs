using MediatR;
using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Common.Application.Abstrations.Messaging;
public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
