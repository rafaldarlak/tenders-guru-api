using Microsoft.Extensions.Logging;
using Tenders.Guru.Domain.Services;
using Tenders.Guru.Http.Client.Http;

namespace Tenders.Guru.Infrastructure.Services;

public class TenderSyncService : ITenderSyncService
{
    private readonly ILogger<TenderSyncService> _logger;
    private readonly ITenderGuruApiClient _apiClient;

    public TenderSyncService(ILogger<TenderSyncService> logger, ITenderGuruApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public async Task SyncTendersAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Starting tender synchronization at {Timestamp}", DateTime.UtcNow);

            var tenders = await _apiClient.GetTendersAsync(1, 100, cancellationToken);
            await Task.Delay(1000, cancellationToken);
            
            _logger.LogInformation("Tender synchronization completed successfully at {Timestamp}", DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during tender synchronization");
            throw;
        }
    }
}
