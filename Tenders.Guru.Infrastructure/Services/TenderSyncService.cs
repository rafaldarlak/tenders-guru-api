using Microsoft.Extensions.Logging;
using Tenders.Guru.Domain.Entities;
using Tenders.Guru.Domain.Repositories;
using Tenders.Guru.Domain.Services;
using Tenders.Guru.Http.Client.Http;

namespace Tenders.Guru.Infrastructure.Services;

public class TenderSyncService : ITenderSyncService
{
    private readonly ILogger<TenderSyncService> _logger;
    private readonly ITenderGuruApiClient _apiClient;
    private readonly ITendersRepository _tendersRepository;
    private readonly ISupplierRepository _supplierRepository;
    const int MaxPages = 1;

    public TenderSyncService(
        ILogger<TenderSyncService> logger,
        ITenderGuruApiClient apiClient, 
        ITendersRepository tendersRepository, 
        ISupplierRepository supplierRepository)
    {
        _logger = logger;
        _apiClient = apiClient;
        _tendersRepository = tendersRepository;
        _supplierRepository = supplierRepository;
    }

    public async Task SyncTendersAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Starting tender synchronization at {Timestamp}", DateTime.UtcNow);

            for (var i = 1; i <= MaxPages; i++)
            {
                ///TODO Sync tenders
                var tenders = await _apiClient.GetTendersAsync(i, 100, cancellationToken);
                var suppliers = tenders.SelectMany(ts => ts.TenderSuppliers)
                    .Select(ts => ts.Supplier).DistinctBy(s => s.ExternalId).ToList();

                _supplierRepository.Add(suppliers);
                
                await _tendersRepository.SaveChangesAsync(cancellationToken);
                
                foreach (var tender in tenders)
                {
                    foreach (var tenderSupplier in tender.TenderSuppliers)
                    {
                        var supplier = suppliers.First(s => s.ExternalId == tenderSupplier.Supplier.ExternalId);
                        tenderSupplier.Supplier = null;
                        tenderSupplier.SupplierId = supplier.Id;
                    }
                }
                
                _tendersRepository.Add(tenders);
                await _tendersRepository.SaveChangesAsync(cancellationToken);
                
                await Task.Delay(1000, cancellationToken);
            }
            
            _logger.LogInformation("Tender synchronization completed successfully at {Timestamp}", DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during tender synchronization");
            throw;
        }
    }
}
