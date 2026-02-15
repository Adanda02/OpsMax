using System;

namespace OpsMax.Models.Views
{
    public class CollectionSummaryView
    {
        public int IdOrderCollected { get; set; }
        public DateTime DateCollected { get; set; }
        public int InvoiceNumberID { get; set; }
        public string InvoiceNumber { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int OrderStatusID { get; set; }
        public string Driver { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleReg { get; set; }
        public string UserName { get; set; }
        public DateTime DateStamp { get; set; }
        public string AttachmentPath { get; set; }

        public decimal TotalPurchased { get; set; }
        public decimal TotalCollected { get; set; }
        public decimal UnderCollected { get; set; }
        public decimal OverCollected { get; set; }
        public decimal InvoiceTotal { get; set; }
    }
}
