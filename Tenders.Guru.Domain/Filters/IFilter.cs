namespace Tenders.Guru.Domain.Filters;

public interface IFilter<T> where T : class
{
    public IQueryable<T> Apply(IQueryable<T> items);
}