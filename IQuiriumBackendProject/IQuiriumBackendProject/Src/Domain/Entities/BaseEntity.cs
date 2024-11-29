using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IqueiriumBackendProject.Src.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("created_date", TypeName = "timestamp with time zone")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("updated_date", TypeName = "timestamp with time zone")]
        public DateTime? UpdatedDate { get; set; }
    }
}
