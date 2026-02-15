using System.ComponentModel.DataAnnotations;

namespace OpsMax.Models
{
    public class StkItm
    {
        [Key]
        public int StockLink { get; set; }      // Primary Key in StkItm

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(200)]
        public string Description_1 { get; set; }

        // Add other columns if needed
    }
}
