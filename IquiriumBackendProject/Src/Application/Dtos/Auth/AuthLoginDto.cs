using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.Auth
{
    public class AuthLoginDto
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve estar em um formato válido.")]
        [MaxLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
    }
}
