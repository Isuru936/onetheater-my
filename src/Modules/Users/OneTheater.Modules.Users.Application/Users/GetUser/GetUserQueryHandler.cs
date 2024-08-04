using System.Data.Common;
using Dapper;
using OneTheater.Common.Application.Abstrations.Data;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Users.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection conneciton = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            $"""
                SELECT 
                    id AS {nameof(UserResponse.Id)},
                    username AS {nameof(UserResponse.Username)}
                FROM users.users WHERE Id = @UserId
            """;

        UserResponse? user = await conneciton.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

        return user;
    }
}
