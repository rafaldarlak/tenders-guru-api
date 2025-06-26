using Microsoft.EntityFrameworkCore;
using Tenders.Guru.Domain.Entities;
using Tenders.Guru.Domain.Filters;
using Tenders.Guru.Domain.Repositories;
using Tenders.Guru.Infrastructure.Extensions;

namespace Tenders.Guru.Infrastructure.Repositories;

public class TendersRepository : ITendersRepository
{
    private readonly TendersGuruDbContext _dbContext;

    public TendersRepository(TendersGuruDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Tender>> GetAsync(GetTendersFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Tenders.AsQueryable();
        query = filter.Apply(query)
            .ApplyPagination(filter.PageNumber, filter.PageSize);

        return await query.ToListAsync(cancellationToken);
    }

    public Tender GetBySupplierIdAsync(string supplierId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Add(IList<Tender> tenders)
    {
        _dbContext.Tenders.AddRange(tenders);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}