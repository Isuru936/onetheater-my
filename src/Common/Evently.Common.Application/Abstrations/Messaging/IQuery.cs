using MediatR;
using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Common.Application.Abstrations.Messaging;

public interface IBaseQuery;

public interface IQuery : IRequest<Result>, IBaseQuery;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IBaseQuery;
