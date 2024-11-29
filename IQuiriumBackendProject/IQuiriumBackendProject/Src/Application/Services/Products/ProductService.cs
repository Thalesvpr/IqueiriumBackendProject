using IqueiriumBackendProject.Src.Infrastructure.Data; // Importa o contexto de dados da aplicação
using System.Collections.Generic; // Importa o namespace para trabalhar com coleções genéricas
using System.Threading.Tasks; // Importa o namespace para trabalhar com tarefas assíncronas
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para manipulação de dados
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities; // Importa a entidade de produto
using IqueiriumBackendProject.Src.Application.Dtos.Products; // Importa o DTO relacionado a produtos

namespace IqueiriumBackendProject.Src.Application.Services.Products
{
    public class ProductService // Define a classe de serviço de produtos
    {
        private readonly ApplicationDbContext _context; // Campo para armazenar o contexto de banco de dados

        /// <summary>
        /// Inicializa uma nova instância do serviço <see cref="ProductService"/>.
        /// </summary>
        /// <param name="context">
        /// Instância do <see cref="ApplicationDbContext"/> usada para acessar e manipular os dados relacionados a produtos no banco de dados.
        /// </param>
        public ProductService(ApplicationDbContext context)
        {
            _context = context; // Atribui o contexto passado para o campo _context
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
        public async Task<Product> AddProductAsync(ProductCreateDto productDto)
        {
            // Cria um novo objeto de produto com o nome definido no DTO
            var product = new Product
            {
                Name = productDto.Name
            };

            // Adiciona o produto ao DbSet de produtos no contexto
            _context.Products.Add(product);

            // Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();

            // Retorna o produto recém-adicionado
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
            // Busca o produto no banco de dados usando o ID fornecido
            var product = await _context.Products.FindAsync(id);

            // Retorna o produto encontrado ou null se não existir
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
            // Busca todos os produtos no banco de dados e os retorna como uma lista
            var products = await _context.Products.ToListAsync();

            // Retorna a lista de produtos
            return products;
        }
    }
}
