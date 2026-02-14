using Microsoft.EntityFrameworkCore;
using OpsMax.DTO;
using OpsMax.DTO.ViewModels;
using OpsMax.Models;
using OpsMax.Models.Views;

namespace OpsMax.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =========================================================
        // DB SETS (ENTITIES)
        // =========================================================
        public DbSet<Category> Categories { get; set; }

        public DbSet<CollectionEntity> Collections { get; set; }
        public DbSet<CollectionLineEntity> CollectionLines { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<PaymentSource> PaymentSources { get; set; }
        public DbSet<PaymentSourceDocument> PaymentSourceDocuments { get; set; }

        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Load> Loads { get; set; }
        public DbSet<LoadDocument> LoadDocuments { get; set; }
        public DbSet<CustomerAllocation> CustomerAllocations { get; set; }

        // =========================================================
        // DB SETS (VIEWS – KEYLESS)
        // =========================================================
        public DbSet<CollectionSummaryView> CollectionsSummary { get; set; }

        // =========================================================
        // DB SETS (RAW SQL DTOs – KEYLESS)
        // =========================================================
        public DbSet<InvoiceLineDto> InvoiceLines { get; set; }
        public DbSet<SupplierGRVVM> SupplierGrvs { get; set; }
        public DbSet<GLAccountVM> GLAccounts { get; set; }

        // =========================================================
        // MODEL CONFIGURATION
        // =========================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // CATEGORY
            // -----------------------------
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.Id);
            });

            // -----------------------------
            // COLLECTION HEADER
            // -----------------------------
            modelBuilder.Entity<CollectionEntity>(entity =>
            {
                entity.ToTable("_tblCollection");
                entity.HasKey(e => e.idOrderCollected);
            });

            // -----------------------------
            // COLLECTION LINES
            // -----------------------------
            modelBuilder.Entity<CollectionLineEntity>(entity =>
            {
                entity.ToTable("_tblCollectionLines");
                entity.HasKey(e => e.idOrderLineCollected);

                entity.HasOne(l => l.Collection)
                      .WithMany(h => h.Lines)
                      .HasForeignKey(l => l.OrderCollectedID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // -----------------------------
            // ORDER STATUS
            // -----------------------------
            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("_tblOrderStatus");
                entity.HasKey(e => e.idStatus);
            });

            // -----------------------------
            // PAYMENT SOURCE
            // -----------------------------
            modelBuilder.Entity<PaymentSource>(entity =>
            {
                entity.ToTable("PaymentSources");
                entity.HasKey(e => e.idPaymentSource);

                entity.HasMany(p => p.Documents)
                      .WithOne(d => d.PaymentSource)
                      .HasForeignKey(d => d.PaymentSourceID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // -----------------------------
            // PAYMENT SOURCE DOCUMENTS
            // -----------------------------
            modelBuilder.Entity<PaymentSourceDocument>(entity =>
            {
                entity.ToTable("PaymentSourceDocuments");
                entity.HasKey(e => e.idPaymentSourceDoc);
            });

            // -----------------------------
            // TRUCK
            // -----------------------------
            modelBuilder.Entity<Truck>(entity =>
            {
                entity.ToTable("Trucks");
                entity.HasKey(e => e.idTruck);
            });

            // -----------------------------
            // DRIVER
            // -----------------------------
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Drivers");
                entity.HasKey(e => e.idDrivers);
            });

            // -----------------------------
            // LOAD
            // -----------------------------
            modelBuilder.Entity<Load>(entity =>
            {
                entity.ToTable("Loads");
                entity.HasKey(e => e.idLoad);
            });

            // -----------------------------
            // LOAD DOCUMENT
            // -----------------------------
            modelBuilder.Entity<LoadDocument>(entity =>
            {
                entity.ToTable("LoadDocuments");
                entity.HasKey(e => e.idLoadDocuments);
            });

            // -----------------------------
            // CUSTOMER ALLOCATION
            // -----------------------------
            modelBuilder.Entity<CustomerAllocation>(entity =>
            {
                entity.ToTable("CustomerAllocations");
                entity.HasKey(e => e.idCustomerAllocations);
            });

            // -----------------------------
            // COLLECTION SUMMARY VIEW
            // -----------------------------
            modelBuilder.Entity<CollectionSummaryView>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_CollectionsSummary");
            });

            // -----------------------------
            // RAW SQL DTOs (NO TABLE / VIEW)
            // -----------------------------
            modelBuilder.Entity<InvoiceLineDto>(entity => { entity.HasNoKey(); entity.ToView(null); });
            modelBuilder.Entity<SupplierGRVVM>(entity => { entity.HasNoKey(); entity.ToView(null); });
            modelBuilder.Entity<GLAccountVM>(entity => { entity.HasNoKey(); entity.ToView(null); });

            // -----------------------------
            // SEED DATA
            // -----------------------------
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { idStatus = 1, StatusCode = "Not Collected", StatusDescription = "Not Collected" },
                new OrderStatus { idStatus = 2, StatusCode = "Partially Collected", StatusDescription = "Partially Collected" },
                new OrderStatus { idStatus = 3, StatusCode = "Fully Collected", StatusDescription = "Fully Collected" },
                new OrderStatus { idStatus = 4, StatusCode = "Over Collected", StatusDescription = "Over Collected" }
            );
        }
    }
}
