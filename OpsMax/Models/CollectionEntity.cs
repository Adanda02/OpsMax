using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsMax.Models
{
    [Table("_tblCollection")]
    public class CollectionEntity
    {
        [Key]
        public int idOrderCollected { get; set; }

        public DateTime DateCollected { get; set; }

        public int InvoiceNumberID { get; set; }
        public string InvoiceNumber { get; set; }

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        public DateTime InvoiceDate { get; set; }

        // 🔗 ORDER STATUS (FK → _tblOrderStatus)
        public int OrderStatusID { get; set; }

        public string Driver { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleReg { get; set; }

        public decimal OrderBalance { get; set; }

        public string UserName { get; set; }
        public DateTime DateStamp { get; set; }
        public string AttachmentPath { get; set; }

        // 🔗 Navigation
        public ICollection<CollectionLineEntity> Lines { get; set; } = new List<CollectionLineEntity>();
    }
}
