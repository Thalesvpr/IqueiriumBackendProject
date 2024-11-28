namespace IqueiriumBackendProject.Src.Application.Dtos.Users
{
    /// <summary>
    /// DTO usado para retornar as informações de um usuário.
    /// </summary>
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
