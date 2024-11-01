using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ManyToMany
{
    public class ProductFeedbackAnalysisProductFeedback
    {
        [Key, Column("product_feedback_analysis_id", Order = 0)]
        [ForeignKey("ProductFeedbackAnalysis")]
        public int ProductFeedbackAnalysisId { get; set; }

        [Key, Column("product_feedback_id", Order = 1)]
        [ForeignKey("ProductFeedback")]
        public int ProductFeedbackId { get; set; }

        public ProductFeedbackAnalysis ProductFeedbackAnalysis { get; set; }
        public ProductFeedback ProductFeedback { get; set; }
    }
}
