using System.ComponentModel.DataAnnotations;

namespace BOLMS.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Key]   
        [Required]
        public string Name { get; set; }

        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
