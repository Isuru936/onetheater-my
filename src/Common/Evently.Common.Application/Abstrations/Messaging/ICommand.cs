using MediatR;
using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Common.Application.Abstrations.Messaging;

public interface IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface ICommand : IRequest<Result>, IBaseCommand;
