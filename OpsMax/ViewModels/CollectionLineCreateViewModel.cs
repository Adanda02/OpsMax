using System.ComponentModel.DataAnnotations;

namespace OpsMax.ViewModels
{
    public class CollectionLineCreateViewModel
    {
        [Required]
        public string ItemCode { get; set; }

        public string ItemDescription { get; set; }
        public decimal QtyPurchased { get; set; }
        public decimal QtyCollected { get; set; }
        public decimal LineTotal { get; set; }
        public decimal OrderBalance { get; set; }
    }
}
