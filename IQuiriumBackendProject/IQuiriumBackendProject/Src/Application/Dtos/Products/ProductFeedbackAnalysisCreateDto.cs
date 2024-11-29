using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.Products
{
    public class ProductFeedbackAnalysisCreateDto
    {
        [Required(ErrorMessage = "Os IDs dos feedbacks são obrigatórios.")]
        public List<int> ProductFeedbackIds { get; set; } // Agora é uma lista de IDs

        [Required(ErrorMessage = "O ID do analista é obrigatório.")]
        public int AnalystUserId { get; set; } // Novo campo para o ID do usuário analista

        [Required(ErrorMessage = "O conteúdo da análise é obrigatório.")]
        [MaxLength(500, ErrorMessage = "O conteúdo da análise deve ter no máximo 500 caracteres.")]
        public string Content { get; set; }
    }
}
