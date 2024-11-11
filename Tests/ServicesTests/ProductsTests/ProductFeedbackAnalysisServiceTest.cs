using IqueiriumBackendProject.Src.Application.Dtos.Products; // Importa os DTOs (Data Transfer Objects) relacionados ao feedback de produtos
using IqueiriumBackendProject.Src.Application.Services.Products; // Importa o serviço que lida com a análise de feedbacks de produtos
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities; // Importa as entidades de domínio relacionadas à análise de feedbacks de produtos
using IqueiriumBackendProject.Src.Infrastructure.Data; // Importa o contexto do banco de dados da aplicação
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para a manipulação de dados no banco
using Moq; // Importa o Moq para criação de objetos mock (simulados) para testes
using Xunit; // Importa o framework de testes xUnit

namespace IqueiriumBackendProject.Tests.ServicesTests.ProductsTests // Define o namespace para os testes dos serviços relacionados aos produtos
{
    public class ProductFeedbackAnalysisServiceTest // Define a classe de teste para o serviço de análise de feedback de produtos
    {
        private readonly Mock<ApplicationDbContext> _mockContext; // Declara um mock do contexto de banco de dados
        private readonly ProductFeedbackAnalysisService _analysisService; // Declara o serviço de análise de feedbacks

        public ProductFeedbackAnalysisServiceTest() // Construtor da classe de teste
        {
            _mockContext = new Mock<ApplicationDbContext>(); // Inicializa o mock do contexto de banco de dados
            _analysisService = new ProductFeedbackAnalysisService(_mockContext.Object); // Inicializa o serviço de análise de feedback com o mock
        }

        [Fact] // Indica que o método abaixo é um teste
        public async Task AnalyzeFeedback_ShouldCreateAnalysesSuccessfully() // Teste para verificar se as análises são criadas corretamente
        {
            var analysisDto = new ProductFeedbackAnalysisCreateDto // Cria um DTO (Data Transfer Object) com os dados da análise
            {
                Content = "Analysis content", // Define o conteúdo da análise
                AnalystUserId = 1, // Define o ID do usuário analista
                ProductFeedbackIds = new List<int> { 1, 2, 3 } // Define os IDs dos feedbacks que serão analisados
            };

            var mockSet = new Mock<DbSet<ProductFeedbackAnalysis>>(); // Cria um mock da DbSet de análises de feedback
            _mockContext.Setup(m => m.ProductFeedbackAnalyses).Returns(mockSet.Object); // Configura o mock do contexto para retornar o mock da DbSet de análises
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1); // Configura o mock para simular a gravação no banco (retorna 1, indicando sucesso)

            var result = await _analysisService.AnalyzeFeedback(analysisDto); // Chama o método que analisa os feedbacks e retorna o resultado

            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(3, result.Count); // Verifica se 3 análises foram criadas (correspondente aos 3 IDs de feedback fornecidos)
            Assert.All(result, analysis => // Verifica se cada análise tem o conteúdo e o ID do analista corretos
            {
                Assert.Equal(analysisDto.Content, analysis.Content);
                Assert.Equal(analysisDto.AnalystUserId, analysis.AnalystUserId);
            });
            mockSet.Verify(m => m.Add(It.IsAny<ProductFeedbackAnalysis>()), Times.Exactly(3)); // Verifica se o método Add foi chamado 3 vezes (uma para cada análise)
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Exactly(3)); // Verifica se o SaveChangesAsync foi chamado 3 vezes
        }

        [Fact] // Indica que o método abaixo é um teste
        public async Task GetFeedbackAnalyses_ShouldReturnAnalysesForFeedback() // Teste para verificar se as análises são recuperadas corretamente para um feedback específico
        {
            var feedbackId = 1; // Define o ID do feedback para o qual as análises serão recuperadas
            var analyses = new List<ProductFeedbackAnalysis> // Cria uma lista de análises de feedback
            {
                new ProductFeedbackAnalysis { Id = 1, ProductFeedbackId = feedbackId, Content = "Analysis 1", AnalystUserId = 1, CreatedDate = DateTime.UtcNow },
                new ProductFeedbackAnalysis { Id = 2, ProductFeedbackId = feedbackId, Content = "Analysis 2", AnalystUserId = 2, CreatedDate = DateTime.UtcNow }
            }.AsQueryable(); // Converte a lista em IQueryable para simular uma consulta LINQ no banco

            var mockSet = new Mock<DbSet<ProductFeedbackAnalysis>>(); // Cria um mock da DbSet de análises de feedback
            mockSet.As<IQueryable<ProductFeedbackAnalysis>>().Setup(m => m.Provider).Returns(analyses.Provider); // Configura o mock para retornar as análises simuladas
            mockSet.As<IQueryable<ProductFeedbackAnalysis>>().Setup(m => m.Expression).Returns(analyses.Expression); // Configura a expressão de consulta
            mockSet.As<IQueryable<ProductFeedbackAnalysis>>().Setup(m => m.ElementType).Returns(analyses.ElementType); // Configura o tipo de elemento
            mockSet.As<IQueryable<ProductFeedbackAnalysis>>().Setup(m => m.GetEnumerator()).Returns(analyses.GetEnumerator()); // Configura o enumerador para iterar sobre as análises
            _mockContext.Setup(m => m.ProductFeedbackAnalyses).Returns(mockSet.Object); // Configura o mock do contexto para retornar o mock da DbSet de análises

            var result = await _analysisService.GetFeedbackAnalyses(feedbackId); // Chama o método para recuperar as análises para o feedback

            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(2, result.Count); // Verifica se 2 análises foram retornadas
            Assert.All(result, analysis => // Verifica se todas as análises retornadas têm o ID do feedback correto
            {
                Assert.Equal(feedbackId, analysis.ProductFeedbackId);
            });
        }

        [Fact] // Indica que o método abaixo é um teste
        public async Task GetFeedbackAnalyses_ShouldReturnEmptyList_WhenNoAnalysesExist() // Teste para verificar se a lista de análises está vazia quando não há análises para o feedback
        {
            var feedbackId = 1; // Define o ID do feedback para o qual as análises serão recuperadas
            var mockSet = new Mock<DbSet<ProductFeedbackAnalysis>>(); // Cria um mock da DbSet de análises de feedback
            _mockContext.Setup(m => m.ProductFeedbackAnalyses).Returns(mockSet.Object); // Configura o mock do contexto para retornar o mock da DbSet de análises

            var result = await _analysisService.GetFeedbackAnalyses(feedbackId); // Chama o método para recuperar as análises (espera-se que não haja análises)

            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Empty(result); // Verifica se a lista de resultados está vazia
        }
    }
}
