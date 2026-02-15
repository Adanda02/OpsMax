using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsMax.Models
{
    public class Truck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTruck { get; set; }

        [Required(ErrorMessage = "Registration is required")]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [Required]
        [Range(0.01, 1000)]
        public decimal CapacityTonnes { get; set; }

        [Required]
        [StringLength(100)]
        public string Owner { get; set; }

        [Required]
        public string Status { get; set; } = "Active";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //public ICollection<Load> Loads { get; set; }
    }
}
