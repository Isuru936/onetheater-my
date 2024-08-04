using MediatR;
using OneTheater.Modules.Users.Domain.Abstractions;

namespace OneTheater.Modules.Users.Application.Abstractions.Messaging;

public interface IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface ICommand : IRequest<Result>, IBaseCommand;
