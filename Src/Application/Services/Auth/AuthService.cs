using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Domain.Enums;
using IqueiriumBackendProject.Src.Infrastructure.Auth;
using IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository.Users;
using System.Threading.Tasks;

namespace IqueiriumBackendProject.Src.Application.Services.Auth
{
    public class AuthService
    {
        private readonly JwtService _jwtService;
        private readonly UserRepository _userRepository;

        public AuthService(JwtService jwtService, UserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Autentica um usuário com base no email e senha.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>Token JWT se a autenticação for bem-sucedida, caso contrário, null.</returns>
        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.FindByEmailAsync(email);

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
        /// <param name="user">Usuário a ser verificado.</param>
        /// <param name="password">Senha fornecida.</param>
        /// <returns>True se a senha for válida; caso contrário, false.</returns>
        private bool VerifyPassword(User user, string password)
        {
            // Aqui você deve substituir pela verificação de hash segura em produção
            return user.Password == password; // Exemplo simplificado
        }
    }
}
