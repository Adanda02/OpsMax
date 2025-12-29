namespace BOLMS.DTO.ViewModels
{
    public class CollectionListViewModel
    {
        public int IdOrderCollected { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateCollected { get; set; }
        public decimal QtyCollected { get; set; }
        public decimal OrderBalance { get; set; }
    }

}
