using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Shows.Domain.Customers;

public sealed class CustomerCreatedDomainEvent(Guid userId, string username) : DomainEvent
{
    public Guid CustomerId { get; init; } = userId;
    public string Username { get; init; } = username;
}
