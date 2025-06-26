namespace Tenders.Guru.Domain.Entities;

public class Tender
{
    public Guid Id { get; set; }
    
    public DateTimeOffset Date { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public string ExternalId { get; set; }
    public decimal AmountEuro { get; set; }
    
    // Navigation property for many-to-many relationship
    public ICollection<TenderSupplier> TenderSuppliers { get; set; } = new List<TenderSupplier>();
}