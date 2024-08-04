using MediatR;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Users;

namespace OneTheater.Modules.Users.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Result<User> result = User.Create(request.Id, request.Username);

        if (result.IsSuccess)
        {
            repository.Insert(result.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
