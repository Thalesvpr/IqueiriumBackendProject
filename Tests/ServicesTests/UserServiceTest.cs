using IqueiriumBackendProject.Src.Application.Dtos.Users; // Importa os DTOs (Data Transfer Objects) de usuários
using IqueiriumBackendProject.Src.Application.Services; // Importa os serviços da aplicação
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities; // Importa as entidades de usuário do domínio
using IqueiriumBackendProject.Src.Infrastructure.Data; // Importa a camada de infraestrutura de dados
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para manipulação de dados com o banco
using Moq; // Importa Moq para simulação de objetos em testes
using Xunit; // Importa Xunit, a biblioteca de testes

namespace IqueiriumBackendProject.Tests.ServicesTests
{
    public class UserServiceTest // Define uma classe de teste para o serviço de usuários
    {
        private readonly Mock<ApplicationDbContext> _mockContext; // Mock do contexto de banco de dados
        private readonly UserService _userService; // Instância do serviço de usuários

        public UserServiceTest() // Construtor da classe de teste
        {
            _mockContext = new Mock<ApplicationDbContext>(); // Inicializa o mock do contexto de banco de dados
            _userService = new UserService(_mockContext.Object); // Inicializa o serviço de usuários com o contexto simulado
        }

        [Fact]
        public async Task RegisterUser_ShouldRegisterNewUser() // Teste para registrar um novo usuário
        {
            // Cria um DTO para registrar um usuário com informações fictícias
            var userDto = new UserRegisterDTO { Name = "Test User", Email = "test@example.com", Password = "password" };

            // Mock do DbSet de usuários, representando a tabela de usuários no banco de dados
            var mockSet = new Mock<DbSet<User>>();

            // Configura o mock para retornar o DbSet simulado de usuários
            _mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            // Configura o mock para retornar 1 ao salvar as mudanças, simulando uma inserção bem-sucedida
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            // Chama o método RegisterUser no serviço para registrar o usuário
            var result = await _userService.RegisterUser(userDto);

            // Verifica se o resultado não é nulo
            Assert.NotNull(result);

            // Verifica se o nome do usuário no resultado é igual ao informado no DTO
            Assert.Equal(userDto.Name, result.Name);

            // Verifica se o email do usuário no resultado é igual ao informado no DTO
            Assert.Equal(userDto.Email, result.Email);

            // Verifica se o método Add foi chamado uma vez para adicionar o usuário
            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once);

            // Verifica se o método SaveChangesAsync foi chamado uma vez para salvar as mudanças
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnUser_WhenCredentialsAreCorrect() // Teste para autenticar o usuário com credenciais corretas
        {
            // Cria um DTO de login com email e senha corretos
            var loginDto = new UserLoginDTO { Email = "test@example.com", Password = "password" };

            // Cria um objeto de usuário fictício correspondente às credenciais
            var user = new User { Id = 1, Name = "Test User", Email = "test@example.com", Password = "password" };

            // Mock do DbSet de usuários
            var mockSet = new Mock<DbSet<User>>();

            // Configura o mock para retornar o usuário quando a consulta for executada com as credenciais corretas
            mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), default))
                .ReturnsAsync(user);

            // Configura o mock do contexto para retornar o DbSet simulado de usuários
            _mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            // Chama o método AuthenticateUser para autenticar o usuário
            var result = await _userService.AuthenticateUser(loginDto);

            // Verifica se o resultado não é nulo (autenticação bem-sucedida)
            Assert.NotNull(result);

            // Verifica se o ID do usuário autenticado corresponde ao esperado
            Assert.Equal(user.Id, result.Id);

            // Verifica se o nome do usuário autenticado corresponde ao esperado
            Assert.Equal(user.Name, result.Name);

            // Verifica se o email do usuário autenticado corresponde ao esperado
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnNull_WhenCredentialsAreIncorrect() // Teste para autenticar o usuário com credenciais incorretas
        {
            // Cria um DTO de login com uma senha incorreta
            var loginDto = new UserLoginDTO { Email = "test@example.com", Password = "wrongpassword" };

            // Mock do DbSet de usuários
            var mockSet = new Mock<DbSet<User>>();

            // Configura o mock para retornar null quando as credenciais não corresponderem a um usuário
            mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), default))
                .ReturnsAsync((User)null);

            // Configura o mock do contexto para retornar o DbSet simulado de usuários
            _mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            // Chama o método AuthenticateUser com as credenciais incorretas
            var result = await _userService.AuthenticateUser(loginDto);

            // Verifica se o resultado é nulo (autenticação falhou)
            Assert.Null(result);
        }
    }
}
