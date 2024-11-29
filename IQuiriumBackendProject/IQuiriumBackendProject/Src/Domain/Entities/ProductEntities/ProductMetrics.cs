using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Domain.Entities.ProductEntities
{
    public class ProductMetrics : BaseEntity
    {
        [Required]
        [Column("metric", TypeName = "varchar(50)")]
        public string? Metric { get; set; }

        [Required]
        [Column("value", TypeName = "float")]
        public float Value { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        [Required]
        public int ProductId { get; set; }

        [InverseProperty("ProductMetrics")]
        public Product? Product { get; set; }
    }
}
