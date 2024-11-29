using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Domain.Enums;
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
        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailForAuthAsync(email);

            // Verifica se o usuário existe e se a senha é válida
            if (user == null || !VerifyPassword(user, password))
            {
                return null;
            }

            // Gera o token JWT passando o Id e a role do usuário
            UserRoleType role = user.Role.Type;
            return _jwtService.GenerateToken(user.Id.ToString(), role);
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