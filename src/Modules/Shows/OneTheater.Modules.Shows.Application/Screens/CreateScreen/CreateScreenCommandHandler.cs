using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Screens;

namespace OneTheater.Modules.Shows.Application.Screens.CreateScreen;
internal sealed class CreateScreenCommandHandler(IScreenRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateScreenCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateScreenCommand request, CancellationToken cancellationToken)
    {
        Result<Screen> result = Screen.Create(request.Name, request.TheaterId);

        if (result.IsSuccess)
        {
            repository.Insert(result.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
