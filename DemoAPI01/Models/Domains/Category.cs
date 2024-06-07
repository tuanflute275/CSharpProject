using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI01.Models.Domains
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string CategoryName { get; set; }


        [Column(TypeName = "tinyint")]
        [DefaultValue(1)]
        public int Status { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
