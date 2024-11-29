using System;
using System.Collections.Generic;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using IqueiriumBackendProject.Src.Domain.Entities.MemberFeedbackEntities;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IqueiriumBackendProject.Src.Infrastructure.Data
{
    /// <summary>
    /// Contexto de banco de dados principal para a aplicação, responsável por configurar
    /// as entidades e relacionamentos do Entity Framework Core.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Construtor para inicializar o contexto com as opções especificadas.
        /// </summary>
        /// <param name="options">Opções de configuração do DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Definição dos DbSets que representam as tabelas no banco de dados
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeedback> ProductFeedbacks { get; set; }
        public DbSet<ProductFeedbackAnalysis> ProductFeedbackAnalyses { get; set; }
        public DbSet<ProductMetrics> ProductMetrics { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<MemberFeedback> MemberFeedbacks { get; set; }
        public DbSet<MemberFeedbackReport> MemberFeedbackReports { get; set; }

        /// <summary>
        /// Método para configurar o modelo de dados e relacionamentos.
        /// </summary>
        /// <param name="modelBuilder">Construtor de modelos para configurar as entidades.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuração da entidade UserRole
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => ur.Id); // Define a chave primária
                entity.Property(ur => ur.Name)
                      .IsRequired()
                      .HasMaxLength(50); // Limita o tamanho do campo Name para 50 caracteres
                entity.Property(ur => ur.CreatedDate)
                      .IsRequired();

                // Dados iniciais
                entity.HasData(
                    new UserRole { Id = 1, Name = "Admin", CreatedDate = DateTime.UtcNow },
                    new UserRole { Id = 2, Name = "Member", CreatedDate = DateTime.UtcNow },
                    new UserRole { Id = 3, Name = "Analyst", CreatedDate = DateTime.UtcNow }
                );
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
