using Microsoft.EntityFrameworkCore;
using Tenders.Guru.Domain.Entities;

namespace Tenders.Guru.Infrastructure;

public class TendersGuruDbContext : DbContext
{
    public TendersGuruDbContext(DbContextOptions<TendersGuruDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Tender> Tenders { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<TenderSupplier> TenderSuppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tender>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.ExternalId)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.AmountEuro)
                .HasPrecision(18, 2);
            
            entity.HasIndex(e => e.Date);
            entity.HasIndex(e => e.AmountEuro);
            entity.HasIndex(e => e.ExternalId).IsUnique();
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.ExternalId).HasMaxLength(200);
            
            entity.HasIndex(e => e.ExternalId).IsUnique();
        });

        modelBuilder.Entity<TenderSupplier>(entity =>
        {
            entity.Property(e => e.TenderId)
                  .IsRequired();
            
            entity.Property(e => e.SupplierId)
                  .IsRequired();
            
            entity.HasKey(e => new { e.TenderId, e.SupplierId });
            
            entity.HasOne(e => e.Tender)
                  .WithMany(e => e.TenderSuppliers)
                  .HasForeignKey(e => e.TenderId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Supplier)
                  .WithMany(e => e.TenderSuppliers)
                  .HasForeignKey(e => e.SupplierId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}