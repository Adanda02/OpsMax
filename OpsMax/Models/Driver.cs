using System.ComponentModel.DataAnnotations;

namespace OpsMax.Models
{
    public class Driver
    {
        public int idDrivers { get; set; }

        [Required]
        public string FullName { get; set; }

        public string NationalID { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }

        public DateTime LicenseExpiry { get; set; }

        public string Status { get; set; } = "Active";

        public ICollection<Load> Loads { get; set; }
    }
}
