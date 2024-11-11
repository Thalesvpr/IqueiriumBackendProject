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

        public ProductService(ApplicationDbContext context) // Construtor da classe, recebe o contexto como dependência
        {
            _context = context; // Atribui o contexto passado para o campo _context
        }

        public async Task<Product> AddProductAsync(CreateProductDto productDto) // Método assíncrono para adicionar um produto
        {
            // Cria um novo objeto de produto com o nome definido no DTO
            var product = new Product
            {
                Name = productDto.Name // Define a propriedade 'Name' do produto
            };

            // Adiciona o produto ao DbSet de produtos no contexto
            _context.Products.Add(product);

            // Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();

            // Retorna o produto recém-adicionado
            return product;
        }

        public async Task<Product> GetProductByIdAsync(int id) // Método assíncrono para buscar um produto pelo ID
        {
            // Busca o produto no banco de dados usando o ID fornecido
            var product = await _context.Products.FindAsync(id);

            // Retorna o produto encontrado ou null se não existir
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() // Método assíncrono para obter todos os produtos
        {
            // Busca todos os produtos no banco de dados e os retorna como uma lista
            var products = await _context.Products.ToListAsync();

            // Retorna a lista de produtos
            return products;
        }
    }
}
