using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IqueiriumBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeedbacks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    feedback_type = table.Column<string>(type: "varchar(50)", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeedbacks", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductFeedbacks_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMetrics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    metric = table.Column<string>(type: "varchar(50)", nullable: false),
                    value = table.Column<float>(type: "float", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMetrics", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductMetrics_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeedbackAnalyses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    analyst_user_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeedbackAnalyses", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductFeedbackAnalyses_Users_analyst_user_id",
                        column: x => x.analyst_user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeedbackAnalysisProductFeedbacks",
                columns: table => new
                {
                    product_feedback_analysis_id = table.Column<int>(type: "integer", nullable: false),
                    product_feedback_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeedbackAnalysisProductFeedbacks", x => new { x.product_feedback_analysis_id, x.product_feedback_id });
                    table.ForeignKey(
                        name: "FK_ProductFeedbackAnalysisProductFeedbacks_ProductFeedbackAnal~",
                        column: x => x.product_feedback_analysis_id,
                        principalTable: "ProductFeedbackAnalyses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFeedbackAnalysisProductFeedbacks_ProductFeedbacks_pr~",
                        column: x => x.product_feedback_id,
                        principalTable: "ProductFeedbacks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_date", "name", "updated_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5618), "Produto A", null },
                    { 2, new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5623), "Produto B", null },
                    { 3, new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5627), "Produto C", null },
                    { 4, new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5629), "Produto D", null },
                    { 5, new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5632), "Produto E", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeedbackAnalyses_analyst_user_id",
                table: "ProductFeedbackAnalyses",
                column: "analyst_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeedbackAnalysisProductFeedbacks_product_feedback_id",
                table: "ProductFeedbackAnalysisProductFeedbacks",
                column: "product_feedback_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeedbacks_product_id",
                table: "ProductFeedbacks",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMetrics_product_id",
                table: "ProductMetrics",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_user_id",
                table: "UserRoles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFeedbackAnalysisProductFeedbacks");

            migrationBuilder.DropTable(
                name: "ProductMetrics");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ProductFeedbackAnalyses");

            migrationBuilder.DropTable(
                name: "ProductFeedbacks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
