using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.MemberFeedback
{
    public class MemberFeedbackReportCreateDto
    {
        [Required(ErrorMessage = "O ID do feedback reportado é obrigatório.")]
        public int MemberFeedbackId { get; set; }

        [Required(ErrorMessage = "O ID do membro que está reportando é obrigatório.")]
        public int ReporterId { get; set; }

        [Required(ErrorMessage = "O motivo do report é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O motivo do report deve ter no máximo 100 caracteres.")]
        public string Reason { get; set; }
    }
}
