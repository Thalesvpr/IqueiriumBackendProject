using IqueiriumBackendProject.Src.Domain.Entities;
using IqueiriumBackendProject.Src.Application.Dtos;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ProductEntity> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }
        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        {
            var product = await _context.Products.ToListAsync();
            return product;
        }
    }
}
