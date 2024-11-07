using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;

namespace IqueiriumBackendProject.Src.Domain.Entities.ProductEntities
{
    public class ProductFeedbackAnalysis : BaseEntity
    {
        [ForeignKey("User")]
        [Column("analyst_user_id")]
        [Required]
        public int AnalystUserId { get; set; }

        public User User { get; set; }

        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; }

        [ForeignKey("ProductFeedback")]
        [Column("feedback_id")]
        [Required]
        public int ProductFeedbackId { get; set; }

        public ProductFeedback ProductFeedback { get; set; }
    }
}
