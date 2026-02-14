using Microsoft.EntityFrameworkCore;
using OpsMax.DTO;
using OpsMax.DTO.ViewModels;

namespace OpsMax.Data
{
    public class ZimMealDbContext : DbContext
    {
        public ZimMealDbContext(DbContextOptions<ZimMealDbContext> options)
            : base(options)
        {
        }

        // =====================================================
        // KEYLESS DTOs / VIEW / RAW SQL RESULTS
        // =====================================================
        public DbSet<InvoiceLineDto> InvoiceLineDtos { get; set; }
        public DbSet<SupplierGRVVM> SupplierGrvs { get; set; }
        public DbSet<GLAccountVM> GLAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // Keyless DTOs
            // -----------------------------
            modelBuilder.Entity<InvoiceLineDto>().HasNoKey();
            modelBuilder.Entity<SupplierGRVVM>().HasNoKey();
            modelBuilder.Entity<GLAccountVM>().HasNoKey();
        }
    }
}
