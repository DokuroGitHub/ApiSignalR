using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Participant_Id",
                table: "Participant");

            migrationBuilder.DropIndex(
                name: "IX_Participant_ConversationId",
                table: "Participant");

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Participant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participant_ConversationId_UserId",
                table: "Participant",
                columns: new[] { "ConversationId", "UserId" });

            migrationBuilder.InsertData(
                table: "Participant",
                columns: new[] { "ConversationId", "UserId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Nickname", "Role", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, 1, null, null },
                    { 2, 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Admin hiền lành", 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Participant",
                columns: new[] { "ConversationId", "UserId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Nickname", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 2, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Member số 1", null, null },
                    { 2, 3, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, null, null },
                    { 2, 4, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Participant_ConversationId_UserId",
                table: "Participant");

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumns: new[] { "ConversationId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumns: new[] { "ConversationId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumns: new[] { "ConversationId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumns: new[] { "ConversationId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumns: new[] { "ConversationId", "UserId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Participant",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participant_Id",
                table: "Participant",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Participant",
                columns: new[] { "Id", "ConversationId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Nickname", "Role", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, 1, null, null, 1 },
                    { 2, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Admin hiền lành", 1, null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Participant",
                columns: new[] { "Id", "ConversationId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Nickname", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { 3, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Member số 1", null, null, 2 },
                    { 4, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, null, null, 3 },
                    { 5, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_ConversationId",
                table: "Participant",
                column: "ConversationId");
        }
    }
}
