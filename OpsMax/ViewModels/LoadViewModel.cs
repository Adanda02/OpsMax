using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpsMax.ViewModels
{
    public class LoadViewModel
    {
        public int idLoad { get; set; }

        [Required]
        public int DCLink { get; set; }

        [Required]
        public int StockLink { get; set; }

        [Required]
        public int idTruck { get; set; }

        [Required]
        public int idDriver { get; set; }

        [Required]
        public decimal LoadedQuantity { get; set; }

        [Required]
        public DateTime LoadDate { get; set; }

        public DateTime EstimatedArrivalDate { get; set; }

        public string Status { get; set; }

        // Dropdown Lists
        public IEnumerable<SelectListItem> Suppliers { get; set; }
        public IEnumerable<SelectListItem> Stocks { get; set; }
        public IEnumerable<SelectListItem> Trucks { get; set; }
        public IEnumerable<SelectListItem> Drivers { get; set; }
    }
}
