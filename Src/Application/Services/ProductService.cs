using IqueiriumBackendProject.Src.Domain.Entities;
using IqueiriumBackendProject.Src.Application.Dtos;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using System.Threading.Tasks;

namespace IqueiriumBackendProject.Src.Application.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductEntity> AddProductAsync(CreateProductDto productDto)
        {
            var product = new ProductEntity
            {
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                Status = productDto.Status,
                CreatedAt = productDto.CreatedAt
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
