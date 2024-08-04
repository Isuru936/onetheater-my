using System.Data.Common;
using Npgsql;
using OneTheater.Modules.Users.Application.Abstractions.Data;

namespace OneTheater.Modules.Users.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public NpgsqlConnection Connection { get; }

    public async ValueTask<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        return await dataSource.OpenConnectionAsync(cancellationToken);
    }
}
