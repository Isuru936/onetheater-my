using MassTransit;
using MediatR;
using OneTheater.Modules.Users.Application.Users.GetUser;
using OneTheater.Modules.Users.IntegrationEvents;
using OneTheater.Modules.Users.Domain.Contacts;

namespace OneTheater.Modules.Users.Presentation.Consumers;

public sealed class GetUserContactDetailsConsumer(ISender sender, IContactInfoRepository contactRepo) : IConsumer<GetUserContactDetailsRequest>
{
    public async Task Consume(ConsumeContext<GetUserContactDetailsRequest> context)
    {
        Common.Domain.Abstractions.Result<UserResponse> result = await sender.Send(new GetUserQuery(context.Message.UserId));

        if (result.IsFailure || result.Value is null)
        {
            await context.RespondAsync(new UserContactDetailsResponse
            {
                UserId = context.Message.UserId,
                Email = null,
                Phone = null,
                Address = null
            });
            return;
        }

        ContactInfo? contact = await contactRepo.GetByUserIdAsync(context.Message.UserId, context.CancellationToken);

        await context.RespondAsync(new UserContactDetailsResponse
        {
            UserId = result.Value.Id,
            Email = contact?.Email ?? result.Value.Email,
            Phone = contact?.Phone,
            Address = contact?.Address
        });
    }
}


