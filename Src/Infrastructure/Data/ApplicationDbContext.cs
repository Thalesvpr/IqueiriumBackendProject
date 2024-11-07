using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;

namespace IqueiriumBackendProject.Src.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeedback> ProductFeedbacks { get; set; }
        public DbSet<ProductFeedbackAnalysis> ProductFeedbackAnalyses { get; set; }
        public DbSet<ProductMetrics> ProductMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do relacionamento muitos-para-um entre ProductFeedback e Product
            modelBuilder.Entity<ProductFeedback>()
                .HasOne(pf => pf.Product)
                .WithMany(p => p.ProductFeedbacks)
                .HasForeignKey(pf => pf.ProductId);

            // Configuração do relacionamento muitos-para-um entre ProductFeedback e User
            modelBuilder.Entity<ProductFeedback>()
                .HasOne(pf => pf.User)
                .WithMany(u => u.ProductFeedbacks)
                .HasForeignKey(pf => pf.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração do relacionamento muitos-para-um entre ProductFeedbackAnalysis e ProductFeedback
            modelBuilder.Entity<ProductFeedbackAnalysis>()
                .HasOne(pfa => pfa.ProductFeedback)
                .WithMany(pf => pf.FeedbackAnalyses)
                .HasForeignKey(pfa => pfa.ProductFeedbackId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed para a tabela de produtos e usuários
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "User A", Email = "usera@example.com", Password = "password123" },
                new User { Id = 2, Name = "User B", Email = "userb@example.com", Password = "password123" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Produto A", CreatedDate = DateTime.UtcNow },
                new Product { Id = 2, Name = "Produto B", CreatedDate = DateTime.UtcNow }
            );
        }
    }
}
