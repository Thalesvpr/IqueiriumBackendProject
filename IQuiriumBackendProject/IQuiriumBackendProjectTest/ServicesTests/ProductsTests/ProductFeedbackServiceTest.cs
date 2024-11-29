using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Application.Services.Products;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IquiriumBackendProject.Tests.ServicesTests.ProductsTests
{
    public class ProductFeedbackServiceTest
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly ProductFeedbackService _service;

        public ProductFeedbackServiceTest()
        {
            _mockContext = new Mock<ApplicationDbContext>(
                new DbContextOptions<ApplicationDbContext>())
            { CallBase = true };
            _service = new ProductFeedbackService(_mockContext.Object);
        }

        [Fact]
        public async Task ShouldSubmitFeedbackSuccessfully()
        {
            // Arrange
            var feedbackDto = new ProductFeedbackCreateDTO
            {
                ProductId = 1,
                Content = "Great product!",
                FeedbackType = "Positive",
                UserId = 1
            };

            var mockSet = new Mock<DbSet<ProductFeedback>>();
            _mockContext.Setup(m => m.ProductFeedbacks).Returns(mockSet.Object);
            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _service.SubmitFeedback(feedbackDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(feedbackDto.ProductId, result.ProductId);
            Assert.Equal(feedbackDto.Content, result.Content);
            Assert.Equal(feedbackDto.FeedbackType, result.FeedbackType);
        }

        [Fact]
        public async Task ShouldGetFeedbacksByProductSuccessfully()
        {
            // Arrange
            var productId = 1;
            var mockSet = new Mock<DbSet<ProductFeedback>>();
            var feedbacks = new List<ProductFeedback>
            {
                new ProductFeedback { Id = 1, ProductId = productId, Content = "Good", FeedbackType = "Positive", CreatedDate = DateTime.UtcNow, UserId = 1 },
                new ProductFeedback { Id = 2, ProductId = productId, Content = "Bad", FeedbackType = "Negative", CreatedDate = DateTime.UtcNow, UserId = 2 }
            };

            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.Provider).Returns(feedbacks.AsQueryable().Provider);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.Expression).Returns(feedbacks.AsQueryable().Expression);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.ElementType).Returns(feedbacks.AsQueryable().ElementType);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.GetEnumerator()).Returns(feedbacks.GetEnumerator());

            _mockContext.Setup(m => m.ProductFeedbacks).Returns(mockSet.Object);

            // Act
            var result = await _service.GetFeedbacksByProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
            Assert.Equal(productId, result.First().ProductId);
        }

        [Fact]
        public async Task ShouldGetFeedbackByIdSuccessfully()
        {
            // Arrange
            var feedbackId = 1;
            var feedback = new ProductFeedback
            {
                Id = feedbackId,
                ProductId = 1,
                Content = "Great product!",
                FeedbackType = "Positive",
                CreatedDate = DateTime.UtcNow,
                UserId = 1
            };

            var mockSet = new Mock<DbSet<ProductFeedback>>();
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.Provider).Returns(new List<ProductFeedback> { feedback }.AsQueryable().Provider);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.Expression).Returns(new List<ProductFeedback> { feedback }.AsQueryable().Expression);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.ElementType).Returns(new List<ProductFeedback> { feedback }.AsQueryable().ElementType);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.GetEnumerator()).Returns(new List<ProductFeedback> { feedback }.GetEnumerator());

            _mockContext.Setup(m => m.ProductFeedbacks).Returns(mockSet.Object);

            // Act
            var result = await _service.GetFeedbackByIdAsync(feedbackId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(feedbackId, result.Id);
        }

        [Fact]
        public async Task ShouldReturnNullWhenFeedbackNotFound()
        {
            // Arrange
            var feedbackId = 999; // Invalid ID
            var mockSet = new Mock<DbSet<ProductFeedback>>();
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.Provider).Returns(Enumerable.Empty<ProductFeedback>().AsQueryable().Provider);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.Expression).Returns(Enumerable.Empty<ProductFeedback>().AsQueryable().Expression);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.ElementType).Returns(Enumerable.Empty<ProductFeedback>().AsQueryable().ElementType);
            mockSet.As<IQueryable<ProductFeedback>>().Setup(m => m.GetEnumerator()).Returns(Enumerable.Empty<ProductFeedback>().GetEnumerator());

            _mockContext.Setup(m => m.ProductFeedbacks).Returns(mockSet.Object);

            // Act
            var result = await _service.GetFeedbackByIdAsync(feedbackId);

            // Assert
            Assert.Null(result);
        }
    }
}
