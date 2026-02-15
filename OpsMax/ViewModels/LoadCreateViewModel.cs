using Microsoft.AspNetCore.Mvc.Rendering;
using OpsMax.Models;
using System.Collections.Generic;

namespace OpsMax.ViewModels
{
    public class LoadCreateViewModel
    {
        public Load Load { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Vendors { get; set; }   // Populated from Vendor table
        public IEnumerable<SelectListItem> Trucks { get; set; }
        public IEnumerable<SelectListItem> Drivers { get; set; }
    }
}

