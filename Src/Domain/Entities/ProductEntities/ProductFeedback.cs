using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ManyToMany;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;

namespace IqueiriumBackendProject.Src.Domain.Entities.ProductEntities
{
    public class ProductFeedback : BaseEntity
    {
        [ForeignKey("Product")]
        [Column("product_id")]
        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int? UserId { get; set; } // Tornando UserId opcional

        public User User { get; set; } // Propriedade de navegação para o usuário

        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; }

        [Required]
        [Column("feedback_type", TypeName = "varchar(50)")]
        public string FeedbackType { get; set; }

        // Propriedade de navegação para as análises associadas a este feedback
        public ICollection<ProductFeedbackAnalysis> FeedbackAnalyses { get; set; }
    }
}
