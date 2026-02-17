using Microsoft.EntityFrameworkCore;
using OpsMax.DTO;
using OpsMax.Models;
using OpsMax.Models.Views;
using OpsMax.ViewModels;

namespace OpsMax.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // =============================
        // DbSets – Tables
        // =============================

        public DbSet<Load> Loads { get; set; }
        public DbSet<LoadDocument> LoadDocuments { get; set; }
        public DbSet<CustomerAllocation> CustomerAllocations { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CollectionEntity> Collections { get; set; }
        public DbSet<CollectionLineEntity> CollectionLines { get; set; }
        public DbSet<PaymentSource> PaymentSources { get; set; }
        public DbSet<PaymentSourceDocument> PaymentSourceDocuments { get; set; }

        // =============================
        // DbSets – Database Views
        // =============================

        public DbSet<CollectionSummaryView> CollectionsSummary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =============================
            // SQL VIEW CONFIGURATION
            // =============================
            modelBuilder.Entity<CollectionSummaryView>()
                .HasNoKey()
                .ToView("CollectionsSummary");

            // =============================
            // Load
            // =============================
            modelBuilder.Entity<Load>(entity =>
            {
                entity.ToTable("Loads");
                entity.HasKey(e => e.idLoad);

                entity.HasOne(l => l.tTruck)
                      .WithMany()
                      .HasForeignKey(l => l.idTruck)
                      .OnDelete(DeleteBehavior.Restrict);

               entity.HasOne(l => l.dDriver)
                      .WithMany()
                      .HasForeignKey(l => l.idDriver)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // =============================
            // Truck
            // =============================
            modelBuilder.Entity<Truck>()
                .ToTable("Trucks")
                .HasKey(t => t.idTruck);

            // =============================
            // Driver
            // =============================
            modelBuilder.Entity<Driver>()
                .ToTable("Drivers")
                .HasKey(d => d.idDriver);

            // =============================
            // LoadDocument
            // =============================
            modelBuilder.Entity<LoadDocument>()
                .ToTable("LoadDocuments")
                .HasKey(d => d.idLoadDocuments);

            // =============================
            // CustomerAllocation
            // =============================
            modelBuilder.Entity<CustomerAllocation>()
                .ToTable("CustomerAllocations")
                .HasKey(c => c.idCustomerAllocations);

            // =============================
            // Seed OrderStatus
            // =============================
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { idStatus = 1, StatusCode = "Not Collected", StatusDescription = "Not Collected" },
                new OrderStatus { idStatus = 2, StatusCode = "Partially Collected", StatusDescription = "Partially Collected" },
                new OrderStatus { idStatus = 3, StatusCode = "Fully Collected", StatusDescription = "Fully Collected" },
                new OrderStatus { idStatus = 4, StatusCode = "Over Collected", StatusDescription = "Over Collected" }
            );
        }
    }
}
