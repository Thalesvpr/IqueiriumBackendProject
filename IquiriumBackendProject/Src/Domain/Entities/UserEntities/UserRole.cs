using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IqueiriumBackendProject.Src.Domain.Enums;

namespace IqueiriumBackendProject.Src.Domain.Entities.UserEntities
{
    public class UserRole : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public UserRoleType Type { get; set; }


        // Função para converter uma string em UserRoleType
        public static UserRoleType ParseRole(string role)
        {
            if (Enum.TryParse(role, true, out UserRoleType parsedRole))
            {
                return parsedRole;
            }
            throw new ArgumentException($"Role '{role}' is not valid.");
        }

    }
}