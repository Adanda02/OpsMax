using System;
using System.Collections.Generic;

namespace BOLMS.DTO.ViewModels
{
    public class CollectionCreateViewModel
    {
        // ===== HEADER =====
        public long InvoiceNumberID { get; set; }
        public string InvoiceNumber { get; set; }

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        public DateTime InvoiceDate { get; set; }
        public IFormFile Attachment { get; set; }


        // ===== LINES =====
        public List<CollectionLineViewModel> Lines { get; set; }
            = new();

        // ===== FOOTER =====
        public string Driver { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleReg { get; set; }

        public int OrderStatusID { get; internal set; }
        public string OrderNotes { get; set; }
        public string UserName { get; internal set; }
        public string AttachmentPath { get; internal set; }
    }
}
