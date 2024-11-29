using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Domain.Entities;

public class UserRole : BaseEntity
{
    public string Name { get; set; } // Nome da role, como "Admin", "User", etc.

    public ICollection<User> Users { get; set; } // Relacionamento com usuários
}
