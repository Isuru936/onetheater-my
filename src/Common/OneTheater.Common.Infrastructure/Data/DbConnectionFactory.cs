using System.Data.Common;
using Npgsql;
using OneTheater.Common.Application.Abstrations.Data;

namespace OneTheater.Common.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public NpgsqlConnection Connection { get; }

    public async ValueTask<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        return await dataSource.OpenConnectionAsync(cancellationToken);
    }
}
