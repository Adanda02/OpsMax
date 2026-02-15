namespace OpsMax.ViewModels
{
    public class CollectionLineViewModel
    {
        //public int idOrderCollected { get; set; }
        public int ItemCodeID { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Warehouse { get; set; }

        public decimal QtyPurchased { get; set; }
        public decimal QtyCollected { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        public decimal OrderBalance
        {
            get => QtyPurchased - QtyCollected;
            set { }
        }
    }
}
