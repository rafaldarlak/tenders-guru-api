namespace Tenders.Guru.Domain.Entities;

public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int ExternalId { get; set; }
    
    // Navigation property for many-to-many relationship
    public ICollection<TenderSupplier> TenderSuppliers { get; set; } = new List<TenderSupplier>();
}