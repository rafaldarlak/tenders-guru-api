using Tenders.Guru.API.Models;
using Tenders.Guru.API.Pagination;

public class GetTendersQuery : IPagedQuery<TenderDto>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public decimal? PriceFrom { get; init; }
    public decimal? PriceTo { get; init; }
    public DateTimeOffset? DateFrom { get; init; }
    public DateTimeOffset? DateTo { get; init; }
    public string SupplierId { get; init; }
}