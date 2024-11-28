using IqueiriumBackendProject.Src.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Application.Dtos.Products;

namespace IqueiriumBackendProject.Src.Application.Services.Products
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância do serviço <see cref="ProductService"/>.
        /// </summary>
        /// <param name="context">
        /// Instância do <see cref="ApplicationDbContext"/> usada para acessar e manipular os dados relacionados a produtos no banco de dados.
        /// </param>
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo produto ao banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="productDto">
        /// Objeto contendo os dados necessários para criar um novo produto, como o nome do produto.
        /// </param>
        /// <returns>
        /// O objeto <see cref="Product"/> criado, contendo os dados do novo produto após ser salvo no banco de dados.
        /// </returns>
        public async Task<Product> AddProductAsync(CreateProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name // Ajustado para refletir apenas a propriedade 'Name'
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Recupera um produto específico do banco de dados com base no ID fornecido.
        /// </summary>
        /// <param name="id">
        /// O ID do produto a ser recuperado.
        /// </param>
        /// <returns>
        /// O objeto <see cref="Product"/> correspondente ao ID fornecido, ou <c>null</c> caso o produto não seja encontrado.
        /// </returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        /// <summary>
        /// Recupera todos os produtos armazenados no banco de dados.
        /// </summary>
        /// <returns>
        /// Uma coleção de objetos <see cref="Product"/> representando todos os produtos encontrados no banco de dados.
        /// </returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
    }
}
