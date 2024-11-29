using IqueiriumBackendProject.Src.Application.Dtos.Auth;
using IqueiriumBackendProject.Src.Application.Dtos.Users;
using IqueiriumBackendProject.Src.Application.Services.Auth;
using IqueiriumBackendProject.Src.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace IqueiriumBackendProject.Src.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        /// <summary>
        /// Endpoint para autenticar um usuário com email e senha.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Autentica o usuário e obtém o token
                var (token, user) = await _authService.AuthenticateAsync(request.Email, request.Password);
                if (token == null || user == null)
                    return Unauthorized("Credenciais inválidas");

                // Monta a resposta
                var response = new AuthResponseDto
                {
                    Token = token,
                    ExpireIn = DateTime.UtcNow.AddMinutes(60),
                    CurrentUser = new UserResponseDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        Role = user.Role.Name,
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado ao realizar o login: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint para registrar um novo usuário.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Registra o usuário
                var user = await _userService.RegisterUserAsync(request.Name, request.Email, request.Password);

                // Autentica o usuário recém-criado e obtém o token
                var (token, authenticatedUser) = await _authService.AuthenticateAsync(user.Email, request.Password);
                if (token == null || authenticatedUser == null)
                    return StatusCode(500, "Erro ao gerar o token para o usuário registrado.");

                // Monta a resposta
                var response = new AuthResponseDto
                {
                    Token = token,
                    ExpireIn = DateTime.UtcNow.AddMinutes(60),
                    CurrentUser = new UserResponseDto
                    {
                        Id = authenticatedUser.Id,
                        Email = authenticatedUser.Email,
                        Name = authenticatedUser.Name,
                        Role = authenticatedUser.Role.Name,
                    }
                };

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado ao registrar o usuário: {ex.Message}");
            }
        }
    }
}
