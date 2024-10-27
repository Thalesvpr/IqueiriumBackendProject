using System;
using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O título deve ter no máximo 255 caracteres.")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CreatedAt { get; set; }
    }
}
