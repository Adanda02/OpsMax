namespace OpsMax.Models
{
    public class CustomerAllocation
    {
        public int idCustomerAllocations { get; set; }

        public int LoadID { get; set; }
        public Load Load { get; set; }

        // From Sage
        public int CustomerID { get; set; }

        public decimal SellingPrice { get; set; }

        public string QuotationNumber { get; set; }

        public string SalesOrderNumber { get; set; }

        public string Status { get; set; } = "Allocated";

        public DateTime AllocatedDate { get; set; } = DateTime.Now;
    }

}
