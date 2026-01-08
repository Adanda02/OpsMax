using System;
using System.Collections.Generic;

namespace OpsMax.Models
{
    public class PaymentSource
    {
        public int Id { get; set; }
        public string SourceType { get; set; }
        public int AccountId { get; set; }
        public string OrderReference { get; set; }
        public string RaisedBy { get; set; }

        public DateTime DateCaptured { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        // ✅ ADD THIS COLUMN PROPERLY
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<PaymentSourceDocument> Documents { get; set; }
    }
}


