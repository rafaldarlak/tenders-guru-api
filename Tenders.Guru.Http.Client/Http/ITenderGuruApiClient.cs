namespace Tenders.Guru.Http.Client.Http;

using Domain.Entities;

public interface ITenderGuruApiClient
{
    Task<IList<Tender>> GetTendersAsync(int page, int pageSize = 2, CancellationToken cancellationToken = default);
}