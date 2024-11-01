using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ManyToMany;

namespace IqueiriumBackendProject.Src.Domain.Entities.ProductEntities
{
    public class ProductFeedback : BaseEntity
    {
        [ForeignKey("Product")]
        [Column("product_id")]
        [Required]
        public int ProductId { get; set; }

        [InverseProperty("ProductFeedbacks")]
        public Product Product { get; set; }

        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; }

        [Required]
        [Column("feedback_type", TypeName = "varchar(50)")]
        public string FeedbackType { get; set; }

        public ICollection<ProductFeedbackAnalysisProductFeedback> ProductFeedbackAnalysisProductFeedbacks { get; set; }
    }
}
