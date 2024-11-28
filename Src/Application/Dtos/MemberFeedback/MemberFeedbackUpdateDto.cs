using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.MemberFeedback
{
    public class MemberFeedbackUpdateDto
    {
        [Required(ErrorMessage = "O ID do feedback é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O conteúdo do feedback é obrigatório.")]
        [MaxLength(1000, ErrorMessage = "O conteúdo do feedback deve ter no máximo 1000 caracteres.")]
        public string Content { get; set; }
    }
}
