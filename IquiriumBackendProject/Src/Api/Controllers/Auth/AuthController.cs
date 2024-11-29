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
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _authService.AuthenticateAsync(request.Email, request.Password);

                if (token == null)
                {
                    return Unauthorized("Credenciais inválidas");
                }

                var user = await _userService.GetUserByEmailForAuthAsync(request.Email);

                if (user == null)
                {
                    return Unauthorized("Usuário não encontrado.");
                }

                // Cria a resposta utilizando UserResponseDto
                var response = new AuthResponseDto
                {
                    Token = token,
                    ExpireIn = DateTime.UtcNow.AddMinutes(60),
                    CurrentUser = new UserResponseDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        RoleType = user.Role.Type
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
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.RegisterUserAsync(request.Name, request.Email, request.Password);

                var token = await _authService.AuthenticateAsync(user.Email, request.Password);

                if (token == null)
                {
                    return StatusCode(500, "Erro ao gerar o token para o usuário registrado.");
                }

                // Cria a resposta utilizando UserResponseDto
                var response = new AuthResponseDto
                {
                    Token = token,
                    ExpireIn = DateTime.UtcNow.AddMinutes(60),
                    CurrentUser = new UserResponseDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        RoleType = user.Role.Type
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
