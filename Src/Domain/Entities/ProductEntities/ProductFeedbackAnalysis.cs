using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ManyToMany;

namespace IqueiriumBackendProject.Src.Domain.Entities.ProductEntities
{
    public class ProductFeedbackAnalysis : BaseEntity
    {
        [ForeignKey("User")]
        [Column("analyst_user_id")]
        [Required]
        public int AnalystUserId { get; set; }

        // Propriedade de navegação para o usuário que realizou a análise
        public User User { get; set; }

        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; }

        // Relação muitos-para-muitos entre ProductFeedback e ProductFeedbackAnalysis
        public ICollection<ProductFeedbackAnalysisProductFeedback> ProductFeedbackAnalysisProductFeedbacks { get; set; }
    }
}
