using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Theaters;

namespace OneTheater.Modules.Shows.Application.Theaters.CreateTheater;

internal sealed class CreateTheaterCommandHandler(ITheaterRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTheaterCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTheaterCommand request, CancellationToken cancellationToken)
    {
        Result<Theater> result = Theater.Create(request.Name);

        if (result.IsSuccess)
        {
            repository.Insert(result.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
