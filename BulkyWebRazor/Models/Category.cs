using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor.Models
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
            public int DisplayOrder { get; set; }
    }
}
