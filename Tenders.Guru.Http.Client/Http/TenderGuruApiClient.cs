using System.Net.Http.Json;
using Tenders.Guru.Domain.Entities;
using Tenders.Guru.Http.Client.Models;

namespace Tenders.Guru.Http.Client.Http;

public class TenderGuruApiClient : ITenderGuruApiClient
{
    private readonly HttpClient _httpClient;
    
    public TenderGuruApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IList<Tender>> GetTendersAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
         var response = await _httpClient.GetFromJsonAsync<IList<TenderDto>>($"tenders?page={page}&pageSize={pageSize}", cancellationToken);

         return ParseTenders(response)
             .ToList();
    }

    private IEnumerable<Tender> ParseTenders(IList<TenderDto> tenders)
    {
        foreach (var tenderDto in tenders)
        {
            var tender = new Tender
            {
                Id = Guid.CreateVersion7(),
                AmountEuro = tenderDto.AmountEuro,
                Date = tenderDto.Date,
                Description = tenderDto.Description,
                Title = tenderDto.Title,
                ExternalId = tenderDto.Id
            };

            tender.TenderSuppliers = tenderDto.Awards.SelectMany(award => award.Suppliers.Select(supplierDto => new TenderSupplier()
            {
                TenderId = tender.Id,
                Supplier = new Supplier
                {
                    Name = supplierDto.Name,
                    ExternalId = supplierDto.Id
                }
            })).ToList();
            
            yield return tender;
        }
    }
    
}