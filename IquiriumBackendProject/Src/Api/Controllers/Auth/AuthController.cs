using IqueiriumBackendProject.Src.Application.Dtos.Auth;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _authService.AuthenticateAsync(request.Email, request.Password);

            if (token == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            var user = await _userService.GetUserByEmailAsync(request.Email);

            var response = new AuthResponseDto
            {
                Token = token,
                ExpireIn = DateTime.UtcNow.AddMinutes(60),
                CurrentUser = user
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.RegisterUser(request);

                var token = await _authService.AuthenticateAsync(user.Email, request.Password);

                if (token == null)
                {
                    return StatusCode(500, "Erro ao gerar o token para o usuário registrado.");
                }

                var response = new AuthResponseDto
                {
                    Token = token,
                    ExpireIn = DateTime.UtcNow.AddMinutes(60),
                    CurrentUser = user
                };

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro inesperado ao registrar o usuário.");
            }
        }
    }
}