using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ManyToMany;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeedback> ProductFeedbacks { get; set; }
        public DbSet<ProductFeedbackAnalysis> ProductFeedbackAnalyses { get; set; }
        public DbSet<ProductMetrics> ProductMetrics { get; set; }
        public DbSet<ProductFeedbackAnalysisProductFeedback> ProductFeedbackAnalysisProductFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductFeedbackAnalysisProductFeedback>()
                .HasKey(pfapf => new { pfapf.ProductFeedbackAnalysisId, pfapf.ProductFeedbackId });

            modelBuilder.Entity<ProductFeedbackAnalysisProductFeedback>()
                .HasOne(pfapf => pfapf.ProductFeedbackAnalysis)
                .WithMany(pfa => pfa.ProductFeedbackAnalysisProductFeedbacks)
                .HasForeignKey(pfapf => pfapf.ProductFeedbackAnalysisId);

            modelBuilder.Entity<ProductFeedbackAnalysisProductFeedback>()
                .HasOne(pfapf => pfapf.ProductFeedback)
                .WithMany(pf => pf.ProductFeedbackAnalysisProductFeedbacks)
                .HasForeignKey(pfapf => pfapf.ProductFeedbackId);

            // Seed para a tabela de produtos
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Produto A", CreatedDate = DateTime.UtcNow },
                new Product { Id = 2, Name = "Produto B", CreatedDate = DateTime.UtcNow },
                new Product { Id = 3, Name = "Produto C", CreatedDate = DateTime.UtcNow },
                new Product { Id = 4, Name = "Produto D", CreatedDate = DateTime.UtcNow },
                new Product { Id = 5, Name = "Produto E", CreatedDate = DateTime.UtcNow }
            );
        }
    }
}
