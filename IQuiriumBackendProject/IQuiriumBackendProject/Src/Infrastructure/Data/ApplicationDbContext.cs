using System;
using System.Collections.Generic;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Domain.Entities.UserEntities;
using IqueiriumBackendProject.Src.Domain.Entities;
using IqueiriumBackendProject.Src.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using IqueiriumBackendProject.Src.Domain.Entities.MemberFeedbackEntities;

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

            // Configuração do relacionamento um-para-um entre User e UserRole
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany() // Sem navegação de volta para User
                .HasForeignKey(u => u.UserRoleId);

            // Configuração do relacionamento entre MemberFeedback e User (Sender e Recipient)
            modelBuilder.Entity<MemberFeedback>()
                .HasOne(f => f.Sender)
                .WithMany()
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberFeedback>()
                .HasOne(f => f.Recipient)
                .WithMany()
                .HasForeignKey(f => f.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração do relacionamento entre MemberFeedbackReport e MemberFeedback
            modelBuilder.Entity<MemberFeedbackReport>()
                .HasOne(r => r.MemberFeedback)
                .WithMany()
                .HasForeignKey(r => r.MemberFeedbackId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração do relacionamento entre MemberFeedbackReport e User (Reporter)
            modelBuilder.Entity<MemberFeedbackReport>()
                .HasOne(r => r.Reporter)
                .WithMany()
                .HasForeignKey(r => r.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed para roles - Dados iniciais para a tabela UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, Type = UserRoleType.Admin },
                new UserRole { Id = 2, Type = UserRoleType.User },
                new UserRole { Id = 3, Type = UserRoleType.Manager }
            );

            // Seed para usuários - Dados iniciais para a tabela Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Manager A", Email = "usera@example.com", Password = "password123", UserRoleId = 3 }, // User normal
                new User { Id = 2, Name = "User B", Email = "userb@example.com", Password = "password123", UserRoleId = 2 }, // Outro user normal
                new User { Id = 3, Name = "Admin User", Email = "admin@example.com", Password = "admin123", UserRoleId = 1 } // Admin
            );
        }
    }
}
