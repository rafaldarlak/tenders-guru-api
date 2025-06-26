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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tender>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ExternalId).HasMaxLength(100);
            entity.HasIndex(e => e.Date);
            entity.HasIndex(e => e.AmountEuro);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ExternalId).HasMaxLength(100);
        });
    }
}