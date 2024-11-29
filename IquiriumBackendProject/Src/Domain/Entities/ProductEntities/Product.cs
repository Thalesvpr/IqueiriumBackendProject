using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Domain.Entities.ProductEntities
{
    public class Product : BaseEntity
    {
        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        public ICollection<ProductFeedback> ProductFeedbacks { get; set; }
        public ICollection<ProductMetrics> ProductMetrics { get; set; }
    }
}
