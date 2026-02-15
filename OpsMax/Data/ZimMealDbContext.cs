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

        // =====================================================
        // PHYSICAL TABLES (Sage / ZimMeal Database)
        // =====================================================
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<StkItm> StockItems { get; set; }   // Maps to dbo.StkItm


        // =====================================================
        // KEYLESS MODELS (SP Results / Views / Raw SQL DTOs)
        // =====================================================
        public DbSet<InvoiceLineDto> InvoiceLineDtos { get; set; }
        public DbSet<SupplierGRVVM> SupplierGrvs { get; set; }
        public DbSet<GLAccountVM> GLAccounts { get; set; }


        // =====================================================
        // MODEL CONFIGURATION
        // =====================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureSageTables(modelBuilder);
            ConfigureKeylessModels(modelBuilder);
        }


        // =====================================================
        // CONFIGURE PHYSICAL SAGE TABLES
        // =====================================================
        private void ConfigureSageTables(ModelBuilder modelBuilder)
        {
            // -----------------------------
            // Vendor Table
            // -----------------------------
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor", "dbo");
                entity.HasKey(v => v.DCLink);

                entity.Property(v => v.DCLink)
                      .ValueGeneratedNever(); // PK managed by Sage
            });

            // -----------------------------
            // Stock Item Table (StkItm)
            // -----------------------------
            modelBuilder.Entity<StkItm>(entity =>
            {
                entity.ToTable("StkItem", "dbo");   // Correct physical table
                entity.HasKey(s => s.StockLink);

                entity.Property(s => s.StockLink)
                      .ValueGeneratedNever(); // PK managed by Sage
            });
        }


        // =====================================================
        // CONFIGURE KEYLESS MODELS
        // =====================================================
        private void ConfigureKeylessModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceLineDto>().HasNoKey();
            modelBuilder.Entity<SupplierGRVVM>().HasNoKey();
            modelBuilder.Entity<GLAccountVM>().HasNoKey();
        }
    }
}
