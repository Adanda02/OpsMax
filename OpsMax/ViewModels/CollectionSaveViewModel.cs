using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpsMax.ViewModels
{
    public class CollectionSaveViewModel
    {
        // Invoice
        [Required]
        public string InvoiceNumber { get; set; }

        // Collection details
        [Required]
        public string Driver { get; set; }

        public string PhoneNumber { get; set; }
        public string VehicleReg { get; set; }
        public string OrderNotes { get; set; }

        // Lines
        [Required]
        public List<CollectionLineViewModel> Lines { get; set; } = new();
    }
}
