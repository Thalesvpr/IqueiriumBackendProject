using Moq;
using Xunit;
using IqueiriumBackendProject.Src.Application.Services.Products;
using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;

namespace IquiriumBackendProject.Tests.ServicesTests.ProductsTests
{
    public class ProductFeedbackAnalysisServiceTest
    {
        [Fact]
        public async Task ShouldAnalyzeFeedbackSuccessfully()
        {
            // Mock do contexto de banco de dados
            var mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            // Dados de entrada para análise
            var analysisDto = new ProductFeedbackAnalysisCreateDto
            {
                Content = "Análise do feedback",
                AnalystUserId = 1,
                ProductFeedbackIds = new List<int> { 1, 2 }
            };

            // Mock para o DbSet de ProductFeedbackAnalysis
            var mockDbSet = new Mock<DbSet<ProductFeedbackAnalysis>>();

            mockContext.Setup(m => m.ProductFeedbackAnalyses).Returns(mockDbSet.Object);

            // Serviço que será testado
            var service = new ProductFeedbackAnalysisService(mockContext.Object);

            // Chama o método de análise
            var result = await service.AnalyzeFeedback(analysisDto);

            // Verifica os resultados
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Deveria ter duas análises
            Assert.All(result, r => Assert.Equal(analysisDto.Content, r.Content));
            Assert.All(result, r => Assert.Equal(analysisDto.AnalystUserId, r.AnalystUserId));
        }

        [Fact]
        public async Task ShouldGetFeedbackAnalysesSuccessfully()
        {
            // Mock do contexto de banco de dados
            var mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            // Dados fictícios de análises
            var feedbackAnalyses = new List<ProductFeedbackAnalysis>
            {
                new ProductFeedbackAnalysis
                {
                    Id = 1,
                    Content = "Análise 1",
                    ProductFeedbackId = 1,
                    AnalystUserId = 1
                },
                new ProductFeedbackAnalysis
                {
                    Id = 2,
                    Content = "Análise 2",
                    ProductFeedbackId = 1,
                    AnalystUserId = 1
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<ProductFeedbackAnalysis>>();
            mockDbSet.As<IQueryable<ProductFeedbackAnalysis>>().Setup(m => m.Provider).Returns(feedbackAnalyses.Provider);
            mockDbSet.As<IQueryable<ProductFeedbackAnalysis>>().Setup(m => m.Expression).Returns(feedbackAnalyses.Expression);
            mockDbSet.As<IQueryable<ProductFeedbackAnalysis>>().Setup(m => m.ElementType).Returns(feedbackAnalyses.ElementType);
            mockDbSet.As<IQueryable<ProductFeedbackAnalysis>>().Setup(m => m.GetEnumerator()).Returns(feedbackAnalyses.GetEnumerator());

            mockContext.Setup(m => m.ProductFeedbackAnalyses).Returns(mockDbSet.Object);

            // Serviço que será testado
            var service = new ProductFeedbackAnalysisService(mockContext.Object);

            // Chama o método de obtenção de análises
            var result = await service.GetFeedbackAnalyses(1);

            // Verifica os resultados
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Espera 2 análises
            Assert.All(result, r => Assert.Equal(1, r.ProductFeedbackId)); // Espera que o ID de feedback seja 1 para todas as análises
        }
    }
}
