using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.MemberFeedback
{
    public class MemberFeedbackCreateDto
    {
        [Required(ErrorMessage = "O ID do remetente é obrigatório.")]
        public int SenderId { get; set; }

        [Required(ErrorMessage = "O ID do destinatário é obrigatório.")]
        public int RecipientId { get; set; }

        [Required(ErrorMessage = "O tipo do feedback é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O tipo do feedback deve ter no máximo 50 caracteres.")]
        public string FeedbackType { get; set; }

        [Required(ErrorMessage = "O conteúdo do feedback é obrigatório.")]
        [MaxLength(1000, ErrorMessage = "O conteúdo do feedback deve ter no máximo 1000 caracteres.")]
        public string Content { get; set; }
    }

}
