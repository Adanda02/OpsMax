using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsMax.Models
{
    [Table("Vendor")] // Maps this entity to the exact table in the Sage database
    public class Vendor
    {
        [Key] // Primary key
        [Column("DCLink")] // Matches the column in the database
        public int VendorID { get; set; }

        [Column("Name")] // Matches the column in the database
        public int DCLink { get; internal set; }
        public string Name { get; internal set; }

        // Optional: add more columns if needed
    }
}
