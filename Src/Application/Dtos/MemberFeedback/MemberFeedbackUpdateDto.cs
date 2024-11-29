using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.MemberFeedback
{
    public class MemberFeedbackUpdateDto
    {
        [Required(ErrorMessage = "O conteúdo do feedback é obrigatório.")]
        [MaxLength(1000, ErrorMessage = "O conteúdo do feedback deve ter no máximo 1000 caracteres.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "O tipo de feedback é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O tipo de feedback deve ter no máximo 50 caracteres.")]
        public string FeedbackType { get; set; }
    }
}
