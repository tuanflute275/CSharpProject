using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI01.Models.Domains
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string ProductName { get; set; }

        [Column(TypeName = "tinyint")]
        [DefaultValue(1)]
        public int? Status { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? ProductImage { get; set; }

        [Required]
        [Column]
        public decimal ProductPrice { get; set; }

        [Column]
        [DefaultValue(0)]
        public decimal? ProductSale { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
