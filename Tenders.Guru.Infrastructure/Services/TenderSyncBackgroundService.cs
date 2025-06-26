using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tenders.Guru.Domain.Services;

namespace Tenders.Guru.Infrastructure.Services;

public class TenderSyncBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TenderSyncBackgroundService> _logger;
    private readonly TenderSyncOptions _options;

    public TenderSyncBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<TenderSyncBackgroundService> logger,
        IOptions<TenderSyncOptions> options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Tender Sync Background Service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var tenderSyncService = scope.ServiceProvider.GetRequiredService<ITenderSyncService>();
                
                await tenderSyncService.SyncTendersAsync(stoppingToken);
                
                _logger.LogInformation("Next tender sync scheduled in {Minutes} minutes", _options.IntervalMinutes);
                await Task.Delay(TimeSpan.FromMinutes(_options.IntervalMinutes), stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in tender sync background service. Retrying in {Minutes} minutes", _options.IntervalMinutes);
                await Task.Delay(TimeSpan.FromMinutes(_options.IntervalMinutes), stoppingToken);
            }
        }

        _logger.LogInformation("Tender Sync Background Service stopped");
    }
}