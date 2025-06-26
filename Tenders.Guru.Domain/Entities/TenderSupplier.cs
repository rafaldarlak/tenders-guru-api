namespace Tenders.Guru.Domain.Entities;

public class TenderSupplier
{
    public Guid TenderId { get; set; }
    public Tender Tender { get; set; }

    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}
