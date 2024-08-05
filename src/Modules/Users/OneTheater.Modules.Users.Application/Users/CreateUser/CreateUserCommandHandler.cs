using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.PublicApi;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Domain.Users;

namespace OneTheater.Modules.Users.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler(
    IUserRepository repository,
    ICustomersApi customersApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Result<User> result = User.Create(request.Username, request.FirstName, request.LastName, request.Email);

        if (result.IsSuccess)
        {
            repository.Insert(result.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await customersApi.PostAsync(new CustomerCreateRequest()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        }, cancellationToken);

        return result.Value.Id;
    }
}
