﻿// <auto-generated />
using System;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IqueiriumBackendProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ManyToMany.ProductFeedbackAnalysisProductFeedback", b =>
                {
                    b.Property<int>("ProductFeedbackAnalysisId")
                        .HasColumnType("integer")
                        .HasColumnName("product_feedback_analysis_id")
                        .HasColumnOrder(0);

                    b.Property<int>("ProductFeedbackId")
                        .HasColumnType("integer")
                        .HasColumnName("product_feedback_id")
                        .HasColumnOrder(1);

                    b.HasKey("ProductFeedbackAnalysisId", "ProductFeedbackId");

                    b.HasIndex("ProductFeedbackId");

                    b.ToTable("ProductFeedbackAnalysisProductFeedbacks");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5618),
                            Name = "Produto A"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5623),
                            Name = "Produto B"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5627),
                            Name = "Produto C"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5629),
                            Name = "Produto D"
                        },
                        new
                        {
                            Id = 5,
                            CreatedDate = new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5632),
                            Name = "Produto E"
                        });
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("FeedbackType")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("feedback_type");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFeedbacks");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductFeedbackAnalysis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnalystUserId")
                        .HasColumnType("integer")
                        .HasColumnName("analyst_user_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.HasIndex("AnalystUserId");

                    b.ToTable("ProductFeedbackAnalyses");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductMetrics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Metric")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("metric");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.Property<float>("Value")
                        .HasColumnType("float")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductMetrics");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.UserEntities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.UserEntities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ManyToMany.ProductFeedbackAnalysisProductFeedback", b =>
                {
                    b.HasOne("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductFeedbackAnalysis", "ProductFeedbackAnalysis")
                        .WithMany("ProductFeedbackAnalysisProductFeedbacks")
                        .HasForeignKey("ProductFeedbackAnalysisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductFeedback", "ProductFeedback")
                        .WithMany("ProductFeedbackAnalysisProductFeedbacks")
                        .HasForeignKey("ProductFeedbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductFeedback");

                    b.Navigation("ProductFeedbackAnalysis");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductFeedback", b =>
                {
                    b.HasOne("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.Product", "Product")
                        .WithMany("ProductFeedbacks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductFeedbackAnalysis", b =>
                {
                    b.HasOne("IqueiriumBackendProject.Src.Domain.Entities.UserEntities.User", "User")
                        .WithMany("ProductFeedbackAnalyses")
                        .HasForeignKey("AnalystUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductMetrics", b =>
                {
                    b.HasOne("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.Product", "Product")
                        .WithMany("ProductMetrics")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.UserEntities.UserRole", b =>
                {
                    b.HasOne("IqueiriumBackendProject.Src.Domain.Entities.UserEntities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.Product", b =>
                {
                    b.Navigation("ProductFeedbacks");

                    b.Navigation("ProductMetrics");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductFeedback", b =>
                {
                    b.Navigation("ProductFeedbackAnalysisProductFeedbacks");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ProductFeedbackAnalysis", b =>
                {
                    b.Navigation("ProductFeedbackAnalysisProductFeedbacks");
                });

            modelBuilder.Entity("IqueiriumBackendProject.Src.Domain.Entities.UserEntities.User", b =>
                {
                    b.Navigation("ProductFeedbackAnalyses");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
