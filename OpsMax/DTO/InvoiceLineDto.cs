using System;

namespace BOLMS.DTO
{
    public class InvoiceLineDto
    {
        public long InvoiceNumberID { get; set; }      // NUM.AutoIndex (BIGINT)
        public string InvoiceNumber { get; set; }

        public int CustomerID { get; set; }            // AccountID (BIGINT)
        public string CustomerName { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int ItemCodeID { get; set; }            // StockLink (BIGINT)
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }

        public string Warehouse { get; set; }

        public decimal QtyPurchased { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
