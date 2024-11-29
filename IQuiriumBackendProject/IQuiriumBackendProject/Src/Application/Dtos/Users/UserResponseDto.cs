using IqueiriumBackendProject.Src.Domain.Enums;

namespace IqueiriumBackendProject.Src.Application.Dtos.Users
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserRoleType RoleType { get; set; }
    }
}
