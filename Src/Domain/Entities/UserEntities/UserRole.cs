using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Domain.Entities.UserEntities
{
    public class UserRole : BaseEntity
    {
        [ForeignKey("User")]
        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [InverseProperty("UserRoles")]
        public User User { get; set; }
    }
}
