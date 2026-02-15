using OpsMax.Models;

namespace OpsMax.Models
{
    public class Load
    {
        public int idLoad { get; set; }

        // From Sage
        public int DCLink { get; set; }
        public Vendor Vendor { get; set; } // EF navigation property


        public int StockLink { get; set; }
        public StockItem StockItem { get; set; } // EF navigation property
        public decimal LoadedQuantity { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal ShortageQuantity { get; set; }

        public int TruckID { get; set; }
        public Truck Truck { get; set; }

        public int DriverID { get; set; }
        public Driver Driver { get; set; }

        public DateTime LoadDate { get; set; }
        public DateTime EstimatedArrivalDate { get; set; }
        public DateTime? ArrivalDate { get; set; }

        public string Status { get; set; } = "Loaded";

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<LoadDocument> Documents { get; set; }
        public ICollection<CustomerAllocation> Allocations { get; set; }
    }
}
