using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsMax.Models
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDriver { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string NationalID { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LicenseExpiry { get; set; }

        [Required]
        public string Status { get; set; } = "Active";
    }
}
