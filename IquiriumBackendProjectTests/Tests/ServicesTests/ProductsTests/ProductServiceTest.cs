using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Application.Services.Products;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IquiriumBackendProject.Tests.ServicesTests.ProductsTests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task ShouldAddProductSuccessfully()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<ApplicationDbContext>();

            var productDto = new ProductCreateDto
            {
                Name = "New Product"
            };

            mockContext.Setup(c => c.Products).Returns(mockDbSet.Object);

            var productService = new ProductService(mockContext.Object);

            // Act
            var result = await productService.AddProductAsync(productDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Product", result.Name);
            mockDbSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once); // Verifies that Add was called once
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        }

        [Fact]
        public async Task ShouldGetProductByIdSuccessfully()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<ApplicationDbContext>();

            var productId = 1;
            var product = new Product
            {
                Id = productId,
                Name = "Product 1"
            };

            mockContext.Setup(c => c.Products.FindAsync(productId)).ReturnsAsync(product);

            var productService = new ProductService(mockContext.Object);

            // Act
            var result = await productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Product 1", result.Name);
        }

        [Fact]
        public async Task ShouldReturnNullWhenProductNotFound()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<ApplicationDbContext>();

            var productId = 999; // An ID that doesn't exist
            mockContext.Setup(c => c.Products.FindAsync(productId)).ReturnsAsync((Product?)null);

            var productService = new ProductService(mockContext.Object);

            // Act
            var result = await productService.GetProductByIdAsync(productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldGetAllProductsSuccessfully()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<ApplicationDbContext>();

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
            };

            // Mocking ToListAsync method
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.AsQueryable().GetEnumerator());
            mockDbSet.Setup(m => m.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(products);

            mockContext.Setup(c => c.Products).Returns(mockDbSet.Object);

            var productService = new ProductService(mockContext.Object);

            // Act
            var result = await productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}