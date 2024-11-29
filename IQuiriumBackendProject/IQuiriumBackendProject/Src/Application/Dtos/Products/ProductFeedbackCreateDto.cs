using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.Products
{
    public class ProductFeedbackCreateDTO
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "O conteúdo do feedback é obrigatório.")]
        [MaxLength(500, ErrorMessage = "O conteúdo do feedback deve ter no máximo 500 caracteres.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "O ID do Usuario é obrigatório.")]
        public int UserId { get; set; } // Novo campo para o ID do usuário analista

        [Required(ErrorMessage = "O tipo de feedback é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O tipo de feedback deve ter no máximo 50 caracteres.")]
        public string FeedbackType { get; set; } // Exemplo: "sugestão", "comentário", "erro"
    }
}
