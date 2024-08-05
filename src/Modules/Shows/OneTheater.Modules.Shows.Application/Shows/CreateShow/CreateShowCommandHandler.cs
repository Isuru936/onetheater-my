using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Shows;

namespace OneTheater.Modules.Shows.Application.Shows.CreateShow;

internal sealed class CreateShowCommandHandler
    : ICommandHandler<CreateShowCommand, Guid>
{
    public Task<Result<Guid>> Handle(CreateShowCommand request, CancellationToken cancellationToken) =>
        // Get screen seats

        ////Result<Show> result = Show.Create(request.MovieId, );

        ////if (result.IsSuccess)
        ////{
        ////    repository.Insert(result.Value);
        ////}

        ////await unitOfWork.SaveChangesAsync(cancellationToken);

        ////return result.Value.Id;

        Task.FromResult(Result.Success(Guid.NewGuid()));
}
