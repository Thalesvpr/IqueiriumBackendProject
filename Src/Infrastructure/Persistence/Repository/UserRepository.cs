using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
