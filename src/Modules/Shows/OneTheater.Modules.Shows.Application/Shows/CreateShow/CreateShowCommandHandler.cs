using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Shows.Application.Abstractions.Data;
using OneTheater.Modules.Shows.Domain.Seats;
using OneTheater.Modules.Shows.Domain.SeatsInventories;
using OneTheater.Modules.Shows.Domain.Shows;

namespace OneTheater.Modules.Shows.Application.Shows.CreateShow;

internal sealed class CreateShowCommandHandler(
    IShowRepository repository, 
    ISeatRepository seatRepository,
    ISeatsInventoryRepository seatsInventoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateShowCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateShowCommand request, CancellationToken cancellationToken)
    {
        // Get screen seats
        List<Seat> seats = await seatRepository.GetByScreenId(request.ScreenId);

        Result<Show> result = Show.Create(request.MovieId, request.ScreenId, new DateTimeOffset(request.ShowTime), seats);

        if (result.IsSuccess)
        {
            repository.Insert(result.Value);
            seatsInventoryRepository.Insert(result.Value.SeatsInventories);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }

    ////Task.FromResult(Result.Success(Guid.NewGuid()));
}
