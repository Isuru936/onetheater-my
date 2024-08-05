using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Movies;

namespace OneTheater.Modules.Shows.Application.Movies.CreateMovie;

internal sealed class CreateMovieCommandHandler(IMovieRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMovieCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        Result<Movie> result = Movie.Create(request.Name);

        if (result.IsSuccess)
        {
            repository.Insert(result.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
