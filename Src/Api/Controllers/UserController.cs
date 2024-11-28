using IqueiriumBackendProject.Src.Application.Dtos.Users;
using IqueiriumBackendProject.Src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IqueiriumBackendProject.Src.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// Inicializa uma nova instância do controlador <see cref="UserController"/>.
        /// </summary>
        /// <param name="userService">
        /// Instância do <see cref="UserService"/> usada para acessar e manipular os dados do usuário.
        /// </param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Endpoint para registrar um novo usuário no sistema.
        /// </summary>
        /// <param name="userDto">
        /// Objeto contendo os dados necessários para registrar um novo usuário, como nome, email, senha, etc.
        /// </param>
        /// <returns>
        /// Uma resposta HTTP 201 (Created) com o usuário criado e um link para o próprio recurso recém-criado.
        /// </returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> Register(UserRegisterDTO userDto)
        {
            var createdUser = await _userService.RegisterUser(userDto);
            return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Endpoint para autenticar um usuário com base no email e senha fornecidos.
        /// </summary>
        /// <param name="loginDto">
        /// Objeto contendo as credenciais de login do usuário, como email e senha.
        /// </param>
        /// <returns>
        /// Uma resposta HTTP 200 (OK) com os dados do usuário autenticado se as credenciais forem válidas,
        /// ou uma resposta HTTP 401 (Unauthorized) com uma mensagem de erro se as credenciais forem inválidas.
        /// </returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDTO>> Authenticate(UserLoginDTO loginDto)
        {
            var user = await _userService.AuthenticateUser(loginDto);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(user);
        }
    }
}