using System;
using System.ComponentModel.DataAnnotations;

namespace OpsMax.Models
{
    public class Load
    {

        [Key]
        public int idLoad { get; set; }

        public int DCLink { get; set; }          // Supplier (ZimMeal)
        public int StockLink { get; set; }       // Stock (ZimMeal)

        public int idTruck { get; set; }         // OpsMax
        public Truck  tTruck{ get; set; }
        public int idDriver { get; set; }        // OpsMax
        public Driver dDriver { get; set; }

        public decimal LoadedQuantity { get; set; }

        public DateTime LoadDate { get; set; }
        public DateTime EstimatedArrivalDate { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
