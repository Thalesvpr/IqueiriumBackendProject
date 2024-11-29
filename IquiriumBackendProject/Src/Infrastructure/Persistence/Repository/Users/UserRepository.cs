using System.Threading.Tasks;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository.Users
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Busca um usuário por email, incluindo a role associada.
        /// </summary>
        /// <param name="email">Email do usuário a ser buscado.</param>
        /// <returns>Instância de User com a role incluída, ou null se não for encontrado.</returns>
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _context.Set<User>()
                .Include(u => u.Role) // Inclui a propriedade Role
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

