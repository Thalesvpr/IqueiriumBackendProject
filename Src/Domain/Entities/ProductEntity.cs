using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IqueiriumBackendProject.Src.Domain.Entities
{
    [Table("products")]
    public class ProductEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [Column("price", TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column("status")]
        [MaxLength(50)]
        public string Status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
