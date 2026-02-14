using Microsoft.Build.Framework;

namespace OpsMax.Models
{
    public class Truck
    {
        public int idTruck { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        public decimal CapacityTonnes { get; set; }

        public string Owner { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<Load> Loads { get; set; }
    }

}
