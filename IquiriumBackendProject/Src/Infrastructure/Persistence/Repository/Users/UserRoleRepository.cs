using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Domain.Enums;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository.Users
{
    public class UserRoleRepository : BaseRepository<UserRole>
    {
        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<UserRole?> GetRoleByTypeAsync(UserRoleType roleType)
        {
            return await _context.Set<UserRole>().FirstOrDefaultAsync(r => r.Type == roleType);
        }
    }
}
