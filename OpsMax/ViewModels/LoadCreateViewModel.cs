
    using global::OpsMax.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OpsMax.Models;

    namespace OpsMax.ViewModels
    {
        public class LoadCreateViewModel
        {
            public Load Load { get; set; }

            public IEnumerable<SelectListItem> Trucks { get; set; }
            public IEnumerable<SelectListItem> Drivers { get; set; }
        }
    }

