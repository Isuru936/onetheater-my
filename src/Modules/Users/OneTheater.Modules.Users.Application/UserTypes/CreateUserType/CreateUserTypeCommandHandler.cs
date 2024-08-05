using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Users;
using OneTheater.Modules.Users.Domain.UserTypes;

namespace OneTheater.Modules.Users.Application.UserTypes.CreateUserType;
internal sealed class CreateUserTypeCommandHandler(IUserTypeRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserTypeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserTypeCommand request, CancellationToken cancellationToken)
    {
        Result<UserType> result = UserType.Create(request.Name);

        if (result.IsSuccess)
        {
            repository.Insert(result.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
