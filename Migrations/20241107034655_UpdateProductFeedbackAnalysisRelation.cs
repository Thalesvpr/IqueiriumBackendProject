using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IqueiriumBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductFeedbackAnalysisRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFeedbackAnalysisProductFeedbacks");

            migrationBuilder.AddColumn<int>(
                name: "feedback_id",
                table: "ProductFeedbackAnalyses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 7, 3, 46, 54, 902, DateTimeKind.Utc).AddTicks(8532));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 7, 3, 46, 54, 902, DateTimeKind.Utc).AddTicks(8536));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 7, 3, 46, 54, 902, DateTimeKind.Utc).AddTicks(8283));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 7, 3, 46, 54, 902, DateTimeKind.Utc).AddTicks(8294));

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeedbackAnalyses_feedback_id",
                table: "ProductFeedbackAnalyses",
                column: "feedback_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeedbackAnalyses_ProductFeedbacks_feedback_id",
                table: "ProductFeedbackAnalyses",
                column: "feedback_id",
                principalTable: "ProductFeedbacks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeedbackAnalyses_ProductFeedbacks_feedback_id",
                table: "ProductFeedbackAnalyses");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeedbackAnalyses_feedback_id",
                table: "ProductFeedbackAnalyses");

            migrationBuilder.DropColumn(
                name: "feedback_id",
                table: "ProductFeedbackAnalyses");

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 1, 7, 52, 29, 786, DateTimeKind.Utc).AddTicks(3656));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 1, 7, 52, 29, 786, DateTimeKind.Utc).AddTicks(3659));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 1, 7, 52, 29, 786, DateTimeKind.Utc).AddTicks(3520));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 1, 7, 52, 29, 786, DateTimeKind.Utc).AddTicks(3529));

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeedbackAnalysisProductFeedbacks_product_feedback_id",
                table: "ProductFeedbackAnalysisProductFeedbacks",
                column: "product_feedback_id");
        }
    }
}
