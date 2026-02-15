using OpsMax.Enums;

namespace OpsMax.Models
{
    public class LoadDocument
    {
        public int idLoadDocuments { get; set; }

        public int LoadID { get; set; }
        public Load Load { get; set; }

        public DocumentType DocumentType { get; set; }

        public string FilePath { get; set; }

        public string UploadedBy { get; set; }

        public DateTime UploadedDate { get; set; } = DateTime.Now;
    }

}
