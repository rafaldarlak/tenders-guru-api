namespace Tenders.Guru.API.Pagination;

public interface IPagedResponse<TResponse>
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
    IEnumerable<TResponse> Items { get; set; }
}