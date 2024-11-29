using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IqueiriumBackendProject.Src.Domain.Entities.MemberFeedbackEntities
{
    public class MemberFeedback : BaseEntity
    {
        [Required]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public User Sender { get; set; }

        [Required]
        [ForeignKey("Recipient")]
        public int RecipientId { get; set; }
        public User Recipient { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FeedbackType { get; set; } // Ex.: Competências, Comportamentos

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Required]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
