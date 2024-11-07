using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Infrastructure.Persistence.Products
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
