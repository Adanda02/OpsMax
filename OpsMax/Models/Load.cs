using System;
using System.Collections.Generic;

namespace OpsMax.Models
{
    /// <summary>
    /// Represents a Load entity in the system.
    /// Links to Sage/ZimMeal suppliers and stock items, and local Trucks/Drivers.
    /// </summary>
    public class Load
    {
        // =============================
        // Primary Key
        // =============================
        public int idLoad { get; set; }

        // =============================
        // Supplier (From Sage / ZimMeal)
        // =============================
        public int DCLink { get; set; }          // FK to Vendor.DCLink
        public Vendor Vendor { get; set; }       // Navigation property

        // =============================
        // Stock Item (From Sage / ZimMeal)
        // =============================
        public int StockLink { get; set; }       // FK to StkItm.StockLink
        public StkItm StockItem { get; set; }    // Navigation property

        // =============================
        // Transport
        // =============================
        public int idTruck { get; set; }         // FK to Truck
        public Truck Truck { get; set; }         // Navigation property

        public int idDriver { get; set; }        // FK to Driver
        public Driver Driver { get; set; }       // Navigation property

        // =============================
        // Quantities
        // =============================
        public decimal LoadedQuantity { get; set; }
        //public decimal ActualQuantity { get; set; }
        //public decimal ShortageQuantity { get; set; }

        // =============================
        // Dates
        // =============================
        public DateTime LoadDate { get; set; }
        public DateTime? EstimatedArrivalDate { get; set; }
        //public DateTime? ArrivalDate { get; set; }

        // =============================
        // Status & Audit
        // =============================
        public string Status { get; set; } = "Loaded";
        ////public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // =============================
        // Related Data
        // =============================
        public ICollection<LoadDocument> Documents { get; set; } = new List<LoadDocument>();
        public ICollection<CustomerAllocation> Allocations { get; set; } = new List<CustomerAllocation>();
    }
}
