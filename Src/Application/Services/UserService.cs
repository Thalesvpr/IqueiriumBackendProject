using IqueiriumBackendProject.Src.Application.Dtos.Users;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Application.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância do serviço <see cref="UserService"/>.
        /// </summary>
        /// <param name="context">
        /// Instância do <see cref="ApplicationDbContext"/> usada para acessar e manipular os dados do usuário no banco de dados.
        /// </param>
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="userDto">
        /// Objeto contendo os dados necessários para registrar um novo usuário, como nome, email e senha.
        /// </param>
        /// <returns>
        /// Um objeto <see cref="UserResponseDTO"/> contendo os dados do usuário recém-criado, incluindo ID, nome e email.
        /// </returns>
        public async Task<UserResponseDTO> RegisterUser(UserRegisterDTO userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password // Em um sistema real, hash da senha seria necessário
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        /// <summary>
        /// Autentica um usuário com base no email e senha fornecidos.
        /// </summary>
        /// <param name="loginDto">
        /// Objeto contendo as credenciais de login, como email e senha.
        /// </param>
        /// <returns>
        /// Um objeto <see cref="UserResponseDTO"/> com os dados do usuário autenticado, ou <c>null</c> caso as credenciais sejam inválidas.
        /// </returns>
        public async Task<UserResponseDTO> AuthenticateUser(UserLoginDTO loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null) return null;

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
