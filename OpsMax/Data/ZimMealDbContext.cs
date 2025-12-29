using OpsMax.DTO;
using Microsoft.EntityFrameworkCore;

namespace OpsMax.Data
{
    public class ZimMealDbContext : DbContext
    {
        public ZimMealDbContext(DbContextOptions<ZimMealDbContext> options)
            : base(options)
        {
        }

        // =====================================================
        // STORED PROCEDURE / VIEW RESULTS (KEYLESS)
        // =====================================================
        public DbSet<InvoiceLineDto> InvoiceLineDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Stored procedure results do not have primary keys
            modelBuilder.Entity<InvoiceLineDto>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
