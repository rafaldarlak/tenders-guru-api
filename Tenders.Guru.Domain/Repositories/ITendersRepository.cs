using Tenders.Guru.Domain.Entities;
using Tenders.Guru.Domain.Filters;

namespace Tenders.Guru.Domain.Repositories;

public interface ITendersRepository
{
    public Task<IEnumerable<Tender>> GetAsync(GetTendersFilter filter, CancellationToken cancellationToken = default);
    public void Add(IList<Tender> tenders);
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    public Tender GetBySupplierIdAsync(string supplierId, CancellationToken cancellationToken = default);
}