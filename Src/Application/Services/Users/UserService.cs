using System;
using System.Linq;
using System.Threading.Tasks;
using IqueiriumBackendProject.Src.Application.Dtos.Users;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository.Users;
using IqueiriumBackendProject.Src.Domain.Enums;

namespace IqueiriumBackendProject.Src.Application.Services.Users
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserRoleRepository _userRoleRepository;


        public UserService(UserRepository userRepository, UserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Registra um novo usuário com base nas informações fornecidas no DTO.
        /// </summary>
        /// <param name="userDto">DTO contendo as informações do usuário a ser registrado.</param>
        /// <returns>Um UserResponseDto com as informações do usuário registrado.</returns>
        /// <exception cref="InvalidOperationException">Lançado se o email já estiver em uso.</exception>
        public async Task<UserResponseDto> RegisterUser(UserCreateDto userDto)
        {
            // Verifica se o email já está registrado
            var existingUser = await _userRepository.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("O email já está em uso.");
            }

            // Defina a role padrão como User
            var roleType = UserRoleType.User;

            // Busca a role no banco de dados com base no UserRoleType
            var role = await _userRoleRepository.GetRoleByTypeAsync(roleType);

            if (role == null)
            {
                throw new InvalidOperationException("Role não encontrada.");
            }

            // Cria a entidade de usuário a partir do DTO
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password, // Em produção, utilize hashing seguro de senha
                UserRoleId = role.Id // Atribui a role correta ao usuário
            };

            await _userRepository.AddAsync(user);

            // Retorna o UserResponseDto com os dados do usuário registrado
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleType = role.Type
            };
        }

        /// <summary>
        /// Recupera um usuário com base no email fornecido.
        /// </summary>
        /// <param name="email">Email do usuário a ser recuperado.</param>
        /// <returns>Um UserResponseDto com as informações do usuário, ou null se o usuário não for encontrado.</returns>
        public async Task<UserResponseDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleType = user.Role.Type
            };
        }
    }
}
