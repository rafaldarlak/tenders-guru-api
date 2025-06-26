namespace Tenders.Guru.Infrastructure.Extensions;

public static class IQuerableExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int page, int pageSize)
        where T : class
    {
        if (page < 1 || pageSize < 1)
        {
            throw new ArgumentOutOfRangeException("Page and page size must be greater than 0.");
        }

        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}