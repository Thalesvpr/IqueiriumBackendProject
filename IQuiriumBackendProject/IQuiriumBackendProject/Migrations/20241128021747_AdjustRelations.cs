using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IqueiriumBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeedbacks_Users_user_id",
                table: "ProductFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_user_id",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeedbackAnalyses_feedback_id",
                table: "ProductFeedbackAnalyses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_user_id",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "id");

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "id", "created_date", "Type", "updated_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 28, 2, 17, 42, 85, DateTimeKind.Utc).AddTicks(9515), 0, null },
                    { 2, new DateTime(2024, 11, 28, 2, 17, 42, 85, DateTimeKind.Utc).AddTicks(9532), 1, null },
                    { 3, new DateTime(2024, 11, 28, 2, 17, 42, 85, DateTimeKind.Utc).AddTicks(9536), 2, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_date", "name", "UserRoleId" },
                values: new object[] { new DateTime(2024, 11, 28, 2, 17, 42, 86, DateTimeKind.Utc).AddTicks(465), "Meneger A", 3 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_date", "UserRoleId" },
                values: new object[] { new DateTime(2024, 11, 28, 2, 17, 42, 86, DateTimeKind.Utc).AddTicks(473), 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_date", "email", "name", "password", "updated_date", "UserRoleId" },
                values: new object[] { 3, new DateTime(2024, 11, 28, 2, 17, 42, 86, DateTimeKind.Utc).AddTicks(479), "admin@example.com", "Admin User", "admin123", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeedbackAnalyses_feedback_id",
                table: "ProductFeedbackAnalyses",
                column: "feedback_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeedbacks_Users_user_id",
                table: "ProductFeedbacks",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeedbacks_Users_user_id",
                table: "ProductFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserRoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeedbackAnalyses_feedback_id",
                table: "ProductFeedbackAnalyses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "UserRole",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_date", "name", "updated_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 7, 3, 46, 54, 902, DateTimeKind.Utc).AddTicks(8532), "Produto A", null },
                    { 2, new DateTime(2024, 11, 7, 3, 46, 54, 902, DateTimeKind.Utc).AddTicks(8536), "Produto B", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_date", "name" },
                values: new object[] { new DateTime(2024, 11, 7, 3, 46, 54, 902, DateTimeKind.Utc).AddTicks(8283), "User A" });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_user_id",
                table: "UserRole",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeedbacks_Users_user_id",
                table: "ProductFeedbacks",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_user_id",
                table: "UserRole",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
