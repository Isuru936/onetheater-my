using System.Data.Common;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Contacts;

namespace OneTheater.Modules.Users.Application.Users.Contacts;

internal sealed class UpsertContactInfoCommandHandler(IContactInfoRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpsertContactInfoCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpsertContactInfoCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        ContactInfo? existing = await repository.GetByUserIdAsync(request.UserId, cancellationToken);

        if (existing is null)
        {
            Result<ContactInfo> created = ContactInfo.Create(request.UserId, request.Email, request.Phone, request.Address);
            repository.Insert(created.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return created.Value.Id;
        }

        existing.Update(request.Email, request.Phone, request.Address);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return existing.Id;
    }
}



