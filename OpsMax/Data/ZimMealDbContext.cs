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
        // PHYSICAL TABLES IN ZIM MEAL / SAGE DATABASE
        // =====================================================
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<StkItm> StockItems { get; set; }   // Renamed to StockItems for consistency

        // =====================================================
        // KEYLESS MODELS (SP RESULTS / VIEWS / RAW SQL DTOs)
        // =====================================================
        public DbSet<InvoiceLineDto> InvoiceLineDtos { get; set; }
        public DbSet<SupplierGRVVM> SupplierGrvs { get; set; }
        public DbSet<GLAccountVM> GLAccounts { get; set; }
        public object StkItm { get; internal set; }

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
                entity.ToTable("Vendor", "dbo");      // Change schema if needed
                entity.HasKey(v => v.DCLink);
                entity.Property(v => v.DCLink)
                      .ValueGeneratedNever();       // PK is managed by Sage
            });

            // -----------------------------
            // Stock Item Table
            // -----------------------------
            modelBuilder.Entity<StkItm>(entity =>
            {
                entity.ToTable("StkItm", "dbo");     // Correct schema (was dbo, causing error)
                entity.HasKey(s => s.StockLink);
                entity.Property(s => s.StockLink)
                      .ValueGeneratedNever();       // PK is managed by Sage
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
