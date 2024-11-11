using IqueiriumBackendProject.Src.Application.Dtos.Products; // Importa os DTOs relacionados a produtos
using IqueiriumBackendProject.Src.Application.Services.Products; // Importa o serviço de produtos
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities; // Importa as entidades de produto
using IqueiriumBackendProject.Src.Infrastructure.Data; // Importa o contexto de dados da aplicação
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core
using Moq; // Importa a biblioteca Moq para criação de mocks
using Xunit; // Importa o Xunit para testes unitários

namespace IqueiriumBackendProject.Tests.ServicesTests.ProductsTests
{
    public class ProductServiceTest // Define a classe de testes para o serviço de produtos
    {
        private readonly Mock<ApplicationDbContext> _mockContext; // Mock do contexto de banco de dados
        private readonly ProductService _productService; // Instância do serviço de produtos

        public ProductServiceTest() // Construtor da classe de testes
        {
            _mockContext = new Mock<ApplicationDbContext>(); // Inicializa o mock do contexto
            _productService = new ProductService(_mockContext.Object); // Inicializa o serviço de produtos com o contexto simulado
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProductSuccessfully() // Teste para adicionar um produto com sucesso
        {
            // Cria um DTO de produto para adicionar
            var productDto = new CreateProductDto { Name = "Test Product" };

            // Mock do DbSet de produtos, representando a tabela de produtos no banco de dados
            var mockSet = new Mock<DbSet<Product>>();

            // Configura o mock para retornar o DbSet simulado de produtos
            _mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            // Configura o mock para retornar 1 ao salvar as mudanças, simulando uma inserção bem-sucedida
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            // Chama o método AddProductAsync no serviço para adicionar o produto
            var result = await _productService.AddProductAsync(productDto);

            // Verifica se o resultado não é nulo
            Assert.NotNull(result);

            // Verifica se o nome do produto no resultado é igual ao nome fornecido no DTO
            Assert.Equal(productDto.Name, result.Name);

            // Verifica se o método Add foi chamado uma vez para adicionar o produto
            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once);

            // Verifica se o método SaveChangesAsync foi chamado uma vez para salvar as mudanças
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists() // Teste para buscar um produto existente pelo ID
        {
            // Cria um objeto de produto fictício com ID e nome
            var product = new Product { Id = 1, Name = "Test Product" };

            // Mock do DbSet de produtos
            var mockSet = new Mock<DbSet<Product>>();

            // Configura o mock para retornar o produto quando o FindAsync for chamado com o ID correto
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(product);

            // Configura o mock do contexto para retornar o DbSet simulado de produtos
            _mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            // Chama o método GetProductByIdAsync para buscar o produto pelo ID
            var result = await _productService.GetProductByIdAsync(1);

            // Verifica se o resultado não é nulo
            Assert.NotNull(result);

            // Verifica se o ID do produto retornado corresponde ao esperado
            Assert.Equal(product.Id, result.Id);

            // Verifica se o nome do produto retornado corresponde ao esperado
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist() // Teste para retornar null quando o produto não existe
        {
            // Mock do DbSet de produtos
            var mockSet = new Mock<DbSet<Product>>();

            // Configura o mock para retornar null quando o FindAsync for chamado com um ID inexistente
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync((Product)null);

            // Configura o mock do contexto para retornar o DbSet simulado de produtos
            _mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            // Chama o método GetProductByIdAsync com um ID inexistente
            var result = await _productService.GetProductByIdAsync(1);

            // Verifica se o resultado é nulo, indicando que o produto não existe
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts() // Teste para buscar todos os produtos
        {
            // Cria uma lista de produtos fictícia
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
            }.AsQueryable();

            // Mock do DbSet de produtos
            var mockSet = new Mock<DbSet<Product>>();

            // Configura o mock para simular o comportamento de IQueryable para a lista de produtos
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            // Configura o mock do contexto para retornar o DbSet simulado de produtos
            _mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            // Chama o método GetAllProductsAsync para obter todos os produtos
            var result = await _productService.GetAllProductsAsync();

            // Verifica se o resultado não é nulo
            Assert.NotNull(result);

            // Verifica se a contagem de produtos retornados é igual a 2
            Assert.Equal(2, result.Count());

            // Verifica se o nome do primeiro produto retornado corresponde ao esperado
            Assert.Equal("Product 1", result.First().Name);
        }
    }
}
