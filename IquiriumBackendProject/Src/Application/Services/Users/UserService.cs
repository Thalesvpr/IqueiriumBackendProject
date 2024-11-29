using System;
using System.Threading.Tasks;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Application.Services.Users
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Registra um novo usuário com base nas informações fornecidas.
        /// </summary>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="email">Email do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>O usuário registrado.</returns>
        /// <exception cref="InvalidOperationException">Lançado se o email já estiver em uso ou a role padrão não for encontrada.</exception>
        public async Task<User> RegisterUserAsync(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do usuário não pode ser vazio.", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email do usuário não pode ser vazio.", nameof(email));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("A senha do usuário não pode ser vazia.", nameof(password));

            // Verifica se o email já está registrado
            var existingUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("O email já está em uso.");
            }

            // Busca a role padrão (User)
            var role = await _context.UserRoles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name == "Member");

            if (role == null)
            {
                throw new InvalidOperationException("Role padrão 'Member' não encontrada.");
            }

            // Cria a entidade de usuário
            var user = new User
            {
                Name = name,
                Email = email,
                Password = password, // TODO: Em produção, utilize hashing seguro de senha
                UserRoleId = role.Id
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Recupera um usuário completo pelo email, incluindo a role associada.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns>O usuário correspondente ou null se não encontrado.</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email do usuário não pode ser vazio.", nameof(email));

            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Recupera um usuário pelo ID, incluindo a role associada.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>O usuário correspondente ou null se não encontrado.</returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Atualiza a senha de um usuário.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <param name="newPassword">Nova senha do usuário.</param>
        /// <returns>True se a atualização foi bem-sucedida, caso contrário, false.</returns>
        public async Task<bool> UpdatePasswordAsync(int id, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("A nova senha não pode ser vazia.", nameof(newPassword));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            user.Password = newPassword; // TODO: Em produção, utilize hashing seguro de senha
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
