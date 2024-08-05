using System.Data.Common;
using Dapper;
using OneTheater.Common.Application.Abstrations.Data;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Users.Application.UserTypes.GetUserTypes;

internal sealed class GetUserTypesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserTypesQuery, IReadOnlyCollection<UserTypeResponse>>
{
    public async Task<Result<IReadOnlyCollection<UserTypeResponse>>> Handle(
        GetUserTypesQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            $"""
             SELECT
                 id AS {nameof(UserTypeResponse.Id)},
                 name AS {nameof(UserTypeResponse.Name)}
             FROM users.user_types
             """;

        List<UserTypeResponse> userTypes = (await connection.QueryAsync<UserTypeResponse>(sql)).AsList();

        return userTypes;
    }
}
