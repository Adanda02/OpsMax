namespace OpsMax.DTO.ViewModels
{
    public class CollectionSummaryVM
    {
        public int idOrderCollected { get; set; }
        public DateTime DateCollected { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Driver { get; set; }
        public int OrderStatusID { get; set; }
        public string AttachmentPath { get; set; }

        public decimal TotalPurchased { get; set; }
        public decimal TotalCollected { get; set; }
        public decimal UnderCollected { get; set; }
        public decimal OverCollected { get; set; }
        public decimal InvoiceTotal { get; set; }
    }

}
