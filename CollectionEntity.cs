namespace BOLMS.Models
{
    public class CollectionEntity
    {
        public string InvoiceNumber { get; set; }
        public string ItemCode { get; set; }
        public int QtyPurchased { get; set; }
        public int QtyCollected { get; set; }
        public string OrderStatus { get; set; }
        public DateTime DateStamp { get; set; }
    }
}