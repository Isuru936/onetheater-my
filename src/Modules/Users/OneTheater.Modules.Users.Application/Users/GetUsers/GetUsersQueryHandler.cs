using System.Data.Common;
using Dapper;
using OneTheater.Common.Application.Abstrations.Data;
using OneTheater.Common.Application.Abstrations.Messaging;
using OneTheater.Common.Domain.Abstractions;
using OneTheater.Modules.Users.Application.Users.GetUser;

namespace OneTheater.Modules.Users.Application.Users.GetUsers;
internal sealed class GetUsersQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetUsersQuery, List<UserResponse>>
{
    public async Task<Result<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            $"""
                SELECT 
                    id AS {nameof(UserResponse.Id)},
                    username AS {nameof(UserResponse.Username)},
                    first_name AS {nameof(UserResponse.FirstName)},
                    last_name AS {nameof(UserResponse.LastName)},
                    email AS {nameof(UserResponse.Email)}
                FROM users.users
            """;

        var users = (await connection.QueryAsync<UserResponse>(sql)).ToList();

        return Result<List<UserResponse>>.Success(users);
    }
}
