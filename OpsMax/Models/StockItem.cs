using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsMax.Models
{
    public class StockItem
    {
        [Key]
        public int StockLink { get; set; }  // matches PK in StkItm

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(200)]
        public string Description_1 { get; set; }

        // add other columns if needed
    }
}
