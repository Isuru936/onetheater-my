using MediatR;
using OneTheater.Modules.Users.Domain.Abstractions;

namespace OneTheater.Modules.Users.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
