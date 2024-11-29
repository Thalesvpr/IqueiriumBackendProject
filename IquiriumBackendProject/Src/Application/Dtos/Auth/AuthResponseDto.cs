using IqueiriumBackendProject.Src.Application.Dtos.Users;

namespace IqueiriumBackendProject.Src.Application.Dtos.Auth
{
    public class AuthResponseDto 
    {
        public string Token { get; set; }
        public DateTime ExpireIn { get; set; }

        public UserResponseDto? CurrentUser { get; set; }
    }
}
