using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IqueiriumBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class ProductFeedbackUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_user_id",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_user_id",
                table: "UserRole",
                newName: "IX_UserRole_user_id");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "ProductFeedbacks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "id");

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_date", "email", "name", "password", "updated_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 1, 7, 52, 29, 786, DateTimeKind.Utc).AddTicks(3520), "usera@example.com", "User A", "password123", null },
                    { 2, new DateTime(2024, 11, 1, 7, 52, 29, 786, DateTimeKind.Utc).AddTicks(3529), "userb@example.com", "User B", "password123", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeedbacks_user_id",
                table: "ProductFeedbacks",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeedbacks_Users_user_id",
                table: "ProductFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_user_id",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeedbacks_user_id",
                table: "ProductFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "ProductFeedbacks");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_user_id",
                table: "UserRoles",
                newName: "IX_UserRoles_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "id");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5618));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_date", "name", "updated_date" },
                values: new object[,]
                {
                    { 3, new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5627), "Produto C", null },
                    { 4, new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5629), "Produto D", null },
                    { 5, new DateTime(2024, 11, 1, 2, 7, 43, 570, DateTimeKind.Utc).AddTicks(5632), "Produto E", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_user_id",
                table: "UserRoles",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
