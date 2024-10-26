using Iquirium_projeto_backend_dotnet.Src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iquirium_projeto_backend_dotnet.Src.Infrastructure.Data.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet para entidades
        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<FeedbackProductEntity> FeedbackProduct { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais para entidades
        }
    }
}
