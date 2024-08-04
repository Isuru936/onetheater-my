using MediatR;
using OneTheater.Modules.Users.Domain.Abstractions;

namespace OneTheater.Modules.Users.Application.Abstractions.Messaging;

public interface IBaseQuery;

public interface IQuery : IRequest<Result>, IBaseQuery;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IBaseQuery;
