using Tenders.Guru.Domain.Entities;

namespace Tenders.Guru.Domain.Filters;

public class GetTendersFilter : IFilter<Tender>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public decimal? PriceFrom { get; init; }
    public decimal? PriceTo { get; init; }
    public DateTimeOffset? DateFrom { get; init; }
    public DateTimeOffset? DateTo { get; init; }
    public string SupplierId { get; init; }
    
    public IQueryable<Tender> Apply(IQueryable<Tender> query)
    {
        if (DateFrom.HasValue)
        {
            query = query.Where(t => t.Date >= DateFrom.Value);
        }
        
        if (DateTo.HasValue)
        {
            query = query.Where(t => t.Date <= DateTo.Value);
        }
        
        if (PriceFrom.HasValue)
        {
            query = query.Where(t => t.AmountEuro >= PriceFrom.Value);
        }
        
        if (PriceTo.HasValue)
        {
            query = query.Where(t => t.AmountEuro <= PriceTo.Value);
        }
        
        if (!string.IsNullOrEmpty(SupplierId))
        {
            query = query.Where(t => t.ExternalId == SupplierId);
        }
        
        return query;
    }
}