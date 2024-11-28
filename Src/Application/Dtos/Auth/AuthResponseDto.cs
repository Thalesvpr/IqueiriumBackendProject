using IqueiriumBackendProject.Src.Application.Dtos.Users;

namespace IqueiriumBackendProject.Src.Application.Dtos.Auth
{
    /// <summary>
    /// DTO usado para retornar as informações de autenticação após o login.
    /// </summary>
    public class AuthResponseDto 
    {
        public string Token { get; set; }
        public DateTime ExpireIn { get; set; }

        public UserResponseDto CurrentUser { get; set; }
    }
}
