using Microsoft.AspNetCore.Mvc.Rendering;
using OpsMax.Models;
using System.Collections.Generic;

namespace OpsMax.ViewModels
{
    public class LoadCreateViewModel
    {
        // Main Load entity
        public Load Load { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Vendors { get; set; }     // Populated from Vendor table
        public IEnumerable<SelectListItem> StockItems { get; set; }  // Populated from StockItem table
        public IEnumerable<SelectListItem> Trucks { get; set; }      // Populated from Trucks table
        public IEnumerable<SelectListItem> Drivers { get; set; }     // Populated from Drivers table
    }
}
