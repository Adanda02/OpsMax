using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsMax.Models
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        public int DCLink { get; set; }

        public string Name { get; set; }
        public string Account { get; set; }
    }
}
