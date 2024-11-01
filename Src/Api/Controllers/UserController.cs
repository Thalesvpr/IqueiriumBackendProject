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

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> Register(UserRegisterDTO userDto)
        {
            var createdUser = await _userService.RegisterUser(userDto);
            return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost("authenticate")]
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