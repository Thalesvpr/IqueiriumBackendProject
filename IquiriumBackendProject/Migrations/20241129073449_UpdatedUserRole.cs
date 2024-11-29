using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IquiriumBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "UserRoles");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserRoles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "id", "created_date", "Name", "updated_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 29, 7, 34, 48, 928, DateTimeKind.Utc).AddTicks(284), "Admin", null },
                    { 2, new DateTime(2024, 11, 29, 7, 34, 48, 928, DateTimeKind.Utc).AddTicks(385), "Member", null },
                    { 3, new DateTime(2024, 11, 29, 7, 34, 48, 928, DateTimeKind.Utc).AddTicks(386), "Analyst", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserRoles");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "UserRoles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
