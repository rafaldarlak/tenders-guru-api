using Tenders.Guru.Domain.Entities;
using Tenders.Guru.Domain.Repositories;

namespace Tenders.Guru.Infrastructure.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly TendersGuruDbContext _dbContext;

    public SupplierRepository(TendersGuruDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(IList<Supplier> suppliers)
    {
        _dbContext.Suppliers.AddRange(suppliers);
    }

    public void AssignSuppliersToTenders(IList<TenderSupplier> tenderSuppliers)
    {
        _dbContext.TenderSuppliers.AddRange(tenderSuppliers);
    }
}