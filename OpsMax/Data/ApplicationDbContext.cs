using OpsMax.DTO;
using OpsMax.Models;
using Microsoft.EntityFrameworkCore;

namespace OpsMax.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =============================
        // DB SETS (ENTITIES ONLY)
        // =============================
        public DbSet<Category> Categories { get; set; }
        public DbSet<CollectionEntity> Collections { get; set; }
        public DbSet<CollectionLineEntity> CollectionLines { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        // =============================
        // MODEL CONFIGURATION
        // =============================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // Category → Categories
            // -----------------------------
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.Id);
            });

            // -----------------------------
            // Collection HEADER → _tblCollection
            // -----------------------------
            modelBuilder.Entity<CollectionEntity>(entity =>
            {
                entity.ToTable("_tblCollection");
                entity.HasKey(e => e.idOrderCollected);
            });

            // -----------------------------
            // Collection LINES → _tblCollectionLines
            // -----------------------------
            modelBuilder.Entity<CollectionLineEntity>(entity =>
            {
                entity.ToTable("_tblCollectionLines");
                entity.HasKey(e => e.idOrderLineCollected);

                entity.HasOne(l => l.Collection)
                      .WithMany(h => h.Lines)
                      .HasForeignKey(l => l.OrderCollectedID);
            });

            // -----------------------------
            // OrderStatus → _tblOrderStatus
            // -----------------------------
            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("_tblOrderStatus");
                entity.HasKey(e => e.idStatus);
            });

            // -----------------------------
            // DTO: InvoiceLineDto (KEYLESS)
            // -----------------------------
            modelBuilder.Entity<InvoiceLineDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            // -----------------------------
            // SEED ORDER STATUS
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
