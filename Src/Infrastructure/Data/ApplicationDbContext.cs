using IqueiriumBackendProject.Src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais, se necessário


        }
    }
}
