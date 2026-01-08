namespace OpsMax.Models
{
    public class PaymentSourceDocument
    {
        public int Id { get; set; }
        public int PaymentSourceId { get; set; }
        public string DocumentType { get; set; }
        public string FilePath { get; set; }
        public string UploadedBy { get; set; }
        public DateTime DateUploaded { get; set; } = DateTime.Now;

        public PaymentSource PaymentSource { get; set; }
        public DateTime UploadedDate { get; internal set; }
    }

}
