namespace Tenders.Guru.Domain.Services;

public interface ITenderSyncService
{
    Task SyncTendersAsync(CancellationToken cancellationToken = default);
}
