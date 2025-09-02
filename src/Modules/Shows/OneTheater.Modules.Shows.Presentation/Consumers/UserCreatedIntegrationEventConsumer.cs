using System.Threading;
using MassTransit;
using MediatR;
using OneTheater.Common.Application.Abstractions;
using OneTheater.Common.Application.Abstrations.Exceptions;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Application.Customers.CreateCustomer;
using OneTheater.Modules.Users.IntegrationEvents;

namespace OneTheater.Modules.Shows.Presentation.Consumers;
public sealed class UserCreatedIntegrationEventConsumer(ISender sender) : IConsumer<UserCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {

        Result<Guid> result = await sender.Send(new CreateCustomerCommand(
            context.Message.UserId,
            context.Message.FirstName,
            context.Message.LastName,
            context.Message.Email));

        if (result.IsFailure)
        {
            throw new OneTheaterException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
