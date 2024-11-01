using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.Products
{
    public class ProductFeedbackAnalysisCreateDto
    {
        [Required(ErrorMessage = "O ID do feedback é obrigatório.")]
        public int ProductFeedbackId { get; set; }

        [Required(ErrorMessage = "O conteúdo da análise é obrigatório.")]
        [MaxLength(500, ErrorMessage = "O conteúdo da análise deve ter no máximo 500 caracteres.")]
        public string Content { get; set; }
    }
}
