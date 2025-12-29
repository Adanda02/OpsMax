using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpsMax.Models
{
    public class OrderStatus
    {
        [Key]
        public int idStatus { get; set; }

        [Required]
        public string StatusCode { get; set; }

        [Required]
        [StringLength(150)]
        public string StatusDescription { get; set; }

        public ICollection<CollectionEntity> CollectionsEntity { get; set; }
    }
}
