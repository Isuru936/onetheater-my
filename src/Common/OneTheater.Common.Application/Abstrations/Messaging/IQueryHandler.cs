using MediatR;
using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Common.Application.Abstrations.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
