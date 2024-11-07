using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository.Products
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}
