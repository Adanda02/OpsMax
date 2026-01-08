using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpsMax.DTO.ViewModels
{
    public class PaymentSourceCreateVM
    {
        [Required]
        public string SourceType { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        [StringLength(100)]
        public string OrderReference { get; set; }

        // Uploaded documents
        public List<IFormFile> Files { get; set; } = new();

        // Document types matching Files index
        public List<string> DocumentTypes { get; set; } = new();
    }
}
