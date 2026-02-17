using Microsoft.EntityFrameworkCore;
using OpsMax.Models;
using OpsMax.DTO;
using OpsMax.ViewModels;

namespace OpsMax.Data
{
    public class ZimMealDbContext : DbContext
    {
        public ZimMealDbContext(DbContextOptions<ZimMealDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<StkItem> StockItems { get; set; }

        public DbSet<InvoiceLineDto> InvoiceLineDtos { get; set; }
        public DbSet<SupplierGRVVM> SupplierGrvs { get; set; }
        public DbSet<GLAccountVM> GLAccounts { get; set; }
        //public IEnumerable<object> StkItem { get; internal set; }

        //public IEnumerable<object> StkItem { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Vendor
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor", "dbo");
                entity.HasKey(v => v.DCLink);
                entity.Property(v => v.DCLink).ValueGeneratedNever();
            });

            // Stock Item
            modelBuilder.Entity<StkItem>(entity =>
            {
                entity.ToTable("StkItem", "dbo");
                entity.HasKey(s => s.StockLink);
                entity.Property(s => s.StockLink).ValueGeneratedNever();
            });

            // Keyless DTOs
            modelBuilder.Entity<InvoiceLineDto>().HasNoKey();
            modelBuilder.Entity<SupplierGRVVM>().HasNoKey();
            modelBuilder.Entity<GLAccountVM>().HasNoKey();
        }
    }
}
