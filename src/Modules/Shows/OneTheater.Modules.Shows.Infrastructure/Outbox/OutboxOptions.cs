using System.Threading.Tasks;

namespace OneTheater.Modules.Shows.Infrastructure.Outbox;
internal sealed class OutboxOptions
{
    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }
}
