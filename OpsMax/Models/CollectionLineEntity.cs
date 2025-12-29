using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOLMS.Models
{
    [Table("_tblCollectionLines")]
    public class CollectionLineEntity
    {
        [Key]
        public int idOrderLineCollected { get; set; }

        public int OrderCollectedID { get; set; }

        public int ItemCodeID { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Warehouse { get; set; }

        public decimal QtyPurchased { get; set; }
        public decimal QtyCollected { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal OrderBalance { get; set; }

        // 🔗 Navigation
        public CollectionEntity Collection { get; set; }
    }
}
