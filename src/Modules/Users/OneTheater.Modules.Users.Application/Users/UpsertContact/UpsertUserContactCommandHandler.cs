using System.Data.Common;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Users;

namespace OneTheater.Modules.Users.Application.Users.UpsertContact;

internal sealed class UpsertUserContactCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpsertUserContactCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpsertUserContactCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        // For this demo, we treat Email as the contact detail to upsert
        // The domain currently exposes Email as a read-only property; for demo, use repository method to set email.
        await userRepository.UpdateEmailAsync(request.UserId, request.Email, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return user.Id;
    }
}





