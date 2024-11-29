using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IqueiriumBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class MemberAjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberFeedbacks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    RecipientId = table.Column<int>(type: "integer", nullable: false),
                    FeedbackType = table.Column<string>(type: "varchar(50)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFeedbacks", x => x.id);
                    table.ForeignKey(
                        name: "FK_MemberFeedbacks_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberFeedbacks_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberFeedbackReports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberFeedbackId = table.Column<int>(type: "integer", nullable: false),
                    ReporterId = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "varchar(100)", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFeedbackReports", x => x.id);
                    table.ForeignKey(
                        name: "FK_MemberFeedbackReports_MemberFeedbacks_MemberFeedbackId",
                        column: x => x.MemberFeedbackId,
                        principalTable: "MemberFeedbacks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberFeedbackReports_Users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 29, 1, 36, 33, 520, DateTimeKind.Utc).AddTicks(1390));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 29, 1, 36, 33, 520, DateTimeKind.Utc).AddTicks(1408));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 3,
                column: "created_date",
                value: new DateTime(2024, 11, 29, 1, 36, 33, 520, DateTimeKind.Utc).AddTicks(1412));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_date", "name" },
                values: new object[] { new DateTime(2024, 11, 29, 1, 36, 33, 520, DateTimeKind.Utc).AddTicks(2968), "Manager A" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 29, 1, 36, 33, 520, DateTimeKind.Utc).AddTicks(2976));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                column: "created_date",
                value: new DateTime(2024, 11, 29, 1, 36, 33, 520, DateTimeKind.Utc).AddTicks(2982));

            migrationBuilder.CreateIndex(
                name: "IX_MemberFeedbackReports_MemberFeedbackId",
                table: "MemberFeedbackReports",
                column: "MemberFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFeedbackReports_ReporterId",
                table: "MemberFeedbackReports",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFeedbacks_RecipientId",
                table: "MemberFeedbacks",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFeedbacks_SenderId",
                table: "MemberFeedbacks",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberFeedbackReports");

            migrationBuilder.DropTable(
                name: "MemberFeedbacks");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 28, 2, 17, 42, 85, DateTimeKind.Utc).AddTicks(9515));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 28, 2, 17, 42, 85, DateTimeKind.Utc).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "id",
                keyValue: 3,
                column: "created_date",
                value: new DateTime(2024, 11, 28, 2, 17, 42, 85, DateTimeKind.Utc).AddTicks(9536));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_date", "name" },
                values: new object[] { new DateTime(2024, 11, 28, 2, 17, 42, 86, DateTimeKind.Utc).AddTicks(465), "Meneger A" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 28, 2, 17, 42, 86, DateTimeKind.Utc).AddTicks(473));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                column: "created_date",
                value: new DateTime(2024, 11, 28, 2, 17, 42, 86, DateTimeKind.Utc).AddTicks(479));
        }
    }
}
