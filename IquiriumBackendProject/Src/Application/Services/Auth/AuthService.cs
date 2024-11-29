using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Infrastructure.Auth;
using IqueiriumBackendProject.Src.Application.Services.Users;
using System.Threading.Tasks;

namespace IqueiriumBackendProject.Src.Application.Services.Auth
{
    public class AuthService
    {
        private readonly JwtService _jwtService;
        private readonly UserService _userService;

        public AuthService(JwtService jwtService, UserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        /// <summary>
        /// Autentica um usuário com base no email e senha.
        /// </summary>
        /// <returns>Uma tupla contendo o token JWT e o usuário autenticado, ou `null` se a autenticação falhar.</returns>
        public async Task<(string? Token, User? User)> AuthenticateAsync(string email, string password)
        {
            // Obtém o usuário pelo email
            var user = await _userService.GetUserByEmailAsync(email);

            // Verifica se o usuário existe e se a senha é válida
            if (user == null || !VerifyPassword(user, password))
            {
                return (null, null);
            }

            // Gera o token JWT passando o Id e a role do usuário
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Role.Name);

            return (token, user);
        }

        /// <summary>
        /// Verifica se a senha fornecida corresponde ao hash armazenado.
        /// </summary>
        private bool VerifyPassword(User user, string password)
        {
            // Aqui você deve substituir pela verificação de hash segura em produção
            return user.Password == password; // Exemplo simplificado
        }
    }
}
