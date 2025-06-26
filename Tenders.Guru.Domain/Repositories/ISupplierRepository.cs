using Tenders.Guru.Domain.Entities;

namespace Tenders.Guru.Domain.Repositories;

public interface ISupplierRepository
{
    public void Add(IList<Supplier> suppliers);
    public void AssignSuppliersToTenders(IList<TenderSupplier> tenderSuppliers);
}