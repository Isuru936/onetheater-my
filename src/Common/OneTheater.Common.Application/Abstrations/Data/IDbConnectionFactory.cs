using System.Data.Common;

namespace OneTheater.Common.Application.Abstrations.Data;
public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);
}
