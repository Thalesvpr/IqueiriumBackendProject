using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Application.Dtos.Users
{

        public class UserCreateDto
        {
            [Required(ErrorMessage = "O nome é obrigatório.")]
            [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "O email é obrigatório.")]
            [EmailAddress(ErrorMessage = "O email deve estar em um formato válido.")]
            [MaxLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória.")]
            [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
            public string Password { get; set; }
        }
}
