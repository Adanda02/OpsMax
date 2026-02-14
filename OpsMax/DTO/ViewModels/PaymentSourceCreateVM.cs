using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpsMax.DTO.ViewModels
{
    public class PaymentSourceCreateVM
    {
        // -----------------------------
        // SOURCE TYPE
        // -----------------------------
        [Required]
        [StringLength(10)]
        public string SourceType { get; set; } // "AP" or "GL"

        // -----------------------------
        // AP (Accounts Payable)
        // Used when SourceType == "AP"
        // -----------------------------
        public int? SupplierId { get; set; }

        [StringLength(100)]
        public string GrvNumber { get; set; }

        // -----------------------------
        // GL (General Ledger)
        // Used when SourceType == "GL"
        // -----------------------------
        public int? GlAccountId { get; set; }

        [StringLength(100)]
        public string Reference { get; set; }

        // -----------------------------
        // COMMON FIELDS
        // -----------------------------
        [Required]
        public DateTime PaymentDate { get; set; }

        // -----------------------------
        // DOCUMENT UPLOADS
        // -----------------------------
        public List<IFormFile> Files { get; set; } = new();

        // Must match Files by index
        public List<string> DocumentTypes { get; set; } = new();
        public int AccountId { get; internal set; }
        public string OrderReference { get; internal set; }
    }
}
