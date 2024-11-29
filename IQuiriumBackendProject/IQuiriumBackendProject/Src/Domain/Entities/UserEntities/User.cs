using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;

namespace IqueiriumBackendProject.Src.Domain.Entities.UserEntities
{
    public class User : BaseEntity
    {
        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column("email", TypeName = "varchar(100)")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("password", TypeName = "varchar(100)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Chave estrangeira para UserRole
        [ForeignKey("UserRole")]
        public int UserRoleId { get; set; }

        // Propriedade de navegação para UserRole
        public UserRole Role { get; set; }

        public ICollection<ProductFeedback> ProductFeedbacks { get; set; }
        public ICollection<ProductFeedbackAnalysis> ProductFeedbackAnalyses { get; set; }
    }
}
