using System.Data.Common;
using Dapper;
using OneTheater.Modules.Users.Application.Abstractions.Data;
using OneTheater.Modules.Users.Application.Abstractions.Messaging;
using OneTheater.Modules.Users.Domain.Abstractions;

namespace OneTheater.Modules.Users.Application.Users.GetUser;

public class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {

        await using DbConnection conneciton = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            $"""
                SELECT 
                    id AS {nameof(UserResponse.Id)},
                    username AS {nameof(UserResponse.Username)}
                FROM Users WHERE Id = @Id
            """;

        UserResponse? user = await conneciton.QuerySingleOrDefaultAsync(sql, request);

        return user;
    }
}
