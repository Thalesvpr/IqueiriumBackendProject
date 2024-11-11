using IqueiriumBackendProject.Src.Application.Dtos.Products; // Importa o DTO relacionado a feedback de produtos
using IqueiriumBackendProject.Src.Application.Services.Products; // Importa o serviço de feedback de produtos
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities; // Importa as entidades de domínio relacionadas a produtos
using IqueiriumBackendProject.Src.Infrastructure.Data; // Importa o contexto de dados da aplicação
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para manipulação de dados
using Moq; // Importa Moq para criação de objetos mock
using Xunit; // Importa a biblioteca de testes Xunit

namespace IqueiriumBackendProject.Tests.ServicesTests.ProductsTests // Define o namespace para os testes de serviços de produtos
{
    public class ProductFeedbackServiceTest // Define a classe de teste para o serviço de feedback de produtos
    {
        private readonly Mock<ApplicationDbContext> _mockContext; // Mock do contexto de banco de dados
        private readonly ProductFeedbackService _feedbackService; // Instância do serviço de feedback de produtos

        public ProductFeedbackServiceTest() // Construtor da classe de teste
        {
            _mockContext = new Mock<ApplicationDbContext>(); // Inicializa o mock do contexto
            _feedbackService = new ProductFeedbackService(_mockContext.Object); // Cria uma instância do serviço com o contexto mockado
        }

        [Fact]
        public async Task SubmitFeedback_ShouldAddFeedbackSuccessfully() // Teste para adicionar um feedback
        {
            var feedbackDto = new ProductFeedbackCreateDTO // Cria um DTO de feedback com dados de exemplo
            {
                ProductId = 1,
                Content = "Great product!",
                FeedbackType = "Positive",
                UserId = 2
            };

            var mockSet = new Mock<DbSet<ProductFeedback>>(); // Mock do DbSet de feedbacks de produto
            _mockContext.Setup(m => m.ProductFeedbacks).Returns(mockSet.Object); // Configura o mock para retornar o DbSet mockado
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1); // Configura o mock para salvar alterações com sucesso

            var result = await _feedbackService.SubmitFeedback(feedbackDto); // Chama o método para adicionar o feedback

            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(feedbackDto.ProductId, result.ProductId); // Verifica se o ID do produto corresponde ao esperado
            Assert.Equal(feedbackDto.Content, result.Content); // Verifica se o conteúdo do feedback corresponde ao esperado
            Assert.Equal(feedbackDto.FeedbackType, result.FeedbackType); // Verifica se o tipo do feedback corresponde ao esperado
            mockSet.Verify(m => m.Add(It.IsAny<ProductFeedback>()), Times.Once); // Verifica se o feedback foi adicionado ao DbSet uma vez
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once); // Verifica se SaveChangesAsync foi chamado uma vez
        }

        [Fact]
        public async Task GetFeedbacksByProduct_ShouldReturnFeedbacksForProduct() // Teste para obter feedbacks por ID do produto
        {
            var productId = 1; // ID do produto para busca
            var feedbacks = new List<ProductFeedback> // Lista de feedbacks para o produto
            {
                new ProductFeedback { Id = 1, ProductId = productId, Content = "Amazing product!", FeedbackType = "Positive", CreatedDate = DateTime.UtcNow },
                new ProductFeedback { Id = 2, ProductId = productId, Content = "Could be better.", FeedbackType = "Neutral", CreatedDate = DateTime.UtcNow }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ProductFeedback>>(); // Mock do DbSet de feedbacks de produto
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.Provider).Returns(feedbacks.Provider); // Configura o mock para usar a coleção de feedbacks
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.Expression).Returns(feedbacks.Expression);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.ElementType).Returns(feedbacks.ElementType);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.GetEnumerator()).Returns(feedbacks.GetEnumerator());
            _mockContext.Setup(m => m.ProductFeedbacks).Returns(mockSet.Object); // Configura o mock para retornar o DbSet com feedbacks

            var result = await _feedbackService.GetFeedbacksByProduct(productId); // Chama o método para obter feedbacks pelo ID do produto

            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(2, result.Count()); // Verifica se a quantidade de feedbacks está correta
            Assert.All(result, f => Assert.Equal(productId, f.ProductId)); // Verifica se todos os feedbacks são do produto correto
        }

        [Fact]
        public async Task GetFeedbackByIdAsync_ShouldReturnFeedback_WhenFeedbackExists() // Teste para obter feedback por ID
        {
            var feedbackId = 1; // ID do feedback para busca
            var feedback = new ProductFeedback { Id = feedbackId, ProductId = 1, Content = "Excellent!", FeedbackType = "Positive", CreatedDate = DateTime.UtcNow };

            var mockSet = new Mock<DbSet<ProductFeedback>>(); // Mock do DbSet de feedbacks de produto
            mockSet.Setup(m => m.FindAsync(feedbackId)).ReturnsAsync(feedback); // Configura o mock para encontrar o feedback com o ID fornecido
            _mockContext.Setup(m => m.ProductFeedbacks).Returns(mockSet.Object); // Configura o mock para retornar o DbSet com feedbacks

            var result = await _feedbackService.GetFeedbackByIdAsync(feedbackId); // Chama o método para obter o feedback pelo ID

            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(feedbackId, result.Id); // Verifica se o ID do feedback corresponde ao esperado
            Assert.Equal(feedback.Content, result.Content); // Verifica se o conteúdo do feedback corresponde ao esperado
            Assert.Equal(feedback.FeedbackType, result.FeedbackType); // Verifica se o tipo do feedback corresponde ao esperado
        }

        [Fact]
        public async Task GetFeedbackByIdAsync_ShouldReturnNull_WhenFeedbackDoesNotExist() // Teste para verificar retorno nulo quando feedback não existe
        {
            var feedbackId = 1; // ID do feedback inexistente
            var mockSet = new Mock<DbSet<ProductFeedback>>(); // Mock do DbSet de feedbacks de produto
            mockSet.Setup(m => m.FindAsync(feedbackId)).ReturnsAsync((ProductFeedback)null); // Configura o mock para retornar null ao buscar o ID
            _mockContext.Setup(m => m.ProductFeedbacks).Returns(mockSet.Object); // Configura o mock para retornar o DbSet com feedbacks

            var result = await _feedbackService.GetFeedbackByIdAsync(feedbackId); // Chama o método para obter o feedback pelo ID

            Assert.Null(result); // Verifica se o resultado é nulo, como esperado
        }
    }
}
