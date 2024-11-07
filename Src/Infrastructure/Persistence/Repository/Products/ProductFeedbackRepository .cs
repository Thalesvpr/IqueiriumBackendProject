using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Infrastructure.Persistence.Products
{
    public class ProductFeedbackRepository: BaseRepository<ProductFeedback>
    {
        public ProductFeedbackRepository(DbContext context) : base(context)
        {
        }
    }
}
