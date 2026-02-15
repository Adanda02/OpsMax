using System;
using System.Collections.Generic;

namespace OpsMax.Models
{
    public class Load
    {
        // =============================
        // Primary Key
        // =============================
        public int idLoad { get; set; }

        // =============================
        // Supplier (From Sage / ZimMeal)
        // =============================
        public int DCLink { get; set; }     // Maps to Vendor.DCLink
        public Vendor Vendor { get; set; }

        // =============================
        // Stock Item (From Sage / ZimMeal)
        // =============================
        public int StockLink { get; set; }  // Maps to StkItm.StockLink
        public StkItm StockItem { get; set; }

        // =============================
        // Transport
        // =============================
        public int idTruck { get; set; }
        public Truck Truck { get; set; }

        public int idDriver { get; set; }
        public Driver Driver { get; set; }

        // =============================
        // Quantities
        // =============================
        public decimal LoadedQuantity { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal ShortageQuantity { get; set; }

        // =============================
        // Dates
        // =============================
        public DateTime LoadDate { get; set; }
        public DateTime? EstimatedArrivalDate { get; set; }
        public DateTime? ArrivalDate { get; set; }

        // =============================
        // Status & Audit
        // =============================
        public string Status { get; set; } = "Loaded";

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // =============================
        // Related Data
        // =============================
        public ICollection<LoadDocument> Documents { get; set; } = new List<LoadDocument>();
        public ICollection<CustomerAllocation> Allocations { get; set; } = new List<CustomerAllocation>();
    }
}
