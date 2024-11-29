using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Domain.Entities.MemberFeedbackEntities
{
    public class MemberFeedbackReport : BaseEntity
    {
        [Required]
        [ForeignKey("MemberFeedback")]
        public int MemberFeedbackId { get; set; }
        public MemberFeedback MemberFeedback { get; set; }

        [Required]
        [ForeignKey("Reporter")]
        public int ReporterId { get; set; }
        public User Reporter { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Reason { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Pending, Reviewed, ActionTaken

        [Required]
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    }
}
