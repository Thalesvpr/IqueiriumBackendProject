using IqueiriumBackendProject.Src.Application.Dtos.Users; // Importa os DTOs relacionados aos usuários
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities; // Importa as entidades de usuário do domínio
using IqueiriumBackendProject.Src.Infrastructure.Data; // Importa o contexto de dados da aplicação
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para lidar com operações de banco de dados

namespace IqueiriumBackendProject.Src.Application.Services
{
    public class UserService // Define a classe de serviço UserService
    {
        private readonly ApplicationDbContext _context; // Campo privado que representa o contexto de banco de dados

        public UserService(ApplicationDbContext context) // Construtor que recebe o contexto e o atribui ao campo privado
        {
            _context = context;
        }

        public async Task<UserResponseDTO> RegisterUser(UserRegisterDTO userDto) // Método para registrar um novo usuário
        {
            // Cria uma nova instância de User a partir do DTO de registro
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password // Em um sistema real, a senha deveria ser armazenada como hash
            };

            _context.Users.Add(user); // Adiciona o novo usuário ao contexto de banco de dados
            await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados

            // Retorna um DTO com as informações do usuário registrado
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserResponseDTO> AuthenticateUser(UserLoginDTO loginDto) // Método para autenticar um usuário
        {
            // Procura o usuário no banco de dados com o email e senha fornecidos
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null) return null; // Se o usuário não for encontrado, retorna null

            // Retorna um DTO com as informações do usuário autenticado
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
