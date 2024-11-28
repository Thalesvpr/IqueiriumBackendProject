using IqueiriumBackendProject.Src.Application.Dtos.Users;
using IqueiriumBackendProject.Src.Application.Services.Users;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Domain.Enums;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using IqueiriumBackendProject.Src.Infrastructure.Persistence.Repository.Users;
using Moq;
using Xunit;

namespace IqueiriumBackendProject.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task ShouldRegisterUserSuccessfully()
        {
            // Arrange
            var mockUserRepository = new Mock<UserRepository>(Mock.Of<ApplicationDbContext>());
            var mockUserRoleRepository = new Mock<UserRoleRepository>(Mock.Of<ApplicationDbContext>());
            var userDto = new UserCreateDto
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password123",
                RoleType = UserRoleType.User
            };

            var role = new UserRole { Id = 1, Type = UserRoleType.User };

            mockUserRepository.Setup(x => x.FindByEmailAsync(userDto.Email)).ReturnsAsync((User?)null);
            mockUserRoleRepository.Setup(x => x.GetRoleByTypeAsync(userDto.RoleType)).ReturnsAsync(role);
            mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var userService = new UserService(mockUserRepository.Object, mockUserRoleRepository.Object);

            // Act
            var result = await userService.RegisterUser(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userDto.Name, result.Name);
            Assert.Equal(userDto.Email, result.Email);
            Assert.Equal(userDto.RoleType, result.RoleType);
        }

        [Fact]
        public async Task ShouldThrowExceptionIfEmailAlreadyExists()
        {
            // Arrange
            var mockUserRepository = new Mock<UserRepository>(Mock.Of<ApplicationDbContext>());
            var mockUserRoleRepository = new Mock<UserRoleRepository>(Mock.Of<ApplicationDbContext>());
            var userDto = new UserCreateDto
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password123",
                RoleType = UserRoleType.User
            };

            var existingUser = new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password123",
                UserRoleId = 1
            };

            mockUserRepository.Setup(x => x.FindByEmailAsync(userDto.Email)).ReturnsAsync(existingUser);

            var userService = new UserService(mockUserRepository.Object, mockUserRoleRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => userService.RegisterUser(userDto));
        }

        [Fact]
        public async Task ShouldGetUserByEmailSuccessfully()
        {
            // Arrange
            var mockUserRepository = new Mock<UserRepository>(Mock.Of<ApplicationDbContext>());
            var mockUserRoleRepository = new Mock<UserRoleRepository>(Mock.Of<ApplicationDbContext>());
            var user = new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password123",
                UserRoleId = 1
            };

            var role = new UserRole { Id = 1, Type = UserRoleType.User };
            user.Role = role;

            mockUserRepository.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            var userService = new UserService(mockUserRepository.Object, mockUserRoleRepository.Object);

            // Act
            var result = await userService.GetUserByEmailAsync(user.Email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(role.Type, result.RoleType);
        }

        [Fact]
        public async Task ShouldReturnNullIfUserNotFoundByEmail()
        {
            // Arrange
            var mockUserRepository = new Mock<UserRepository>(Mock.Of<ApplicationDbContext>());
            var mockUserRoleRepository = new Mock<UserRoleRepository>(Mock.Of<ApplicationDbContext>());

            mockUserRepository.Setup(x => x.FindByEmailAsync("nonexistent@example.com")).ReturnsAsync((User?)null);

            var userService = new UserService(mockUserRepository.Object, mockUserRoleRepository.Object);

            // Act
            var result = await userService.GetUserByEmailAsync("nonexistent@example.com");

            // Assert
            Assert.Null(result);
        }
    }
}
