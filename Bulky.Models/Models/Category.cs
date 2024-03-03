using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCExample.Models
{
    public class Category
    {
        [Key]
        [DisplayName("Category Id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Category DisplayOrder")]
        public string DisplayOrder { get; set; }
    }
}
