using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1234567890);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), "tamthoidetrong@gmail.com", "Võ", "Thành Đô", "Admin", new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), "dokuro.jp@gmail.com", "Dokuro", "JP", new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), null, "dokuro.jp@gmail.com" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FirstName", "Username" },
                values: new object[] { "dovt58@gmail.com", "dovt58", "dovt58@gmail.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "PasswordHash", "Role", "Token", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 5, null, new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "gidoquenroi1@gmail.com", "gidoquenroi1", "GG", "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "User", null, new DateTime(2023, 5, 6, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "gidoquenroi1@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), "dokuro.jp@gmail.com", "Dokuro", "JP", "User", new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), null, "dokuro.jp@gmail.com" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), "dovt58@gmail.com", "dovt58", "GG", new DateTime(2023, 5, 6, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "dovt58@gmail.com" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FirstName", "Username" },
                values: new object[] { "gidoquenroi1@gmail.com", "gidoquenroi1", "gidoquenroi1@gmail.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "PasswordHash", "Role", "Token", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 1234567890, null, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com", "Võ", "Thành Đô", "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "Admin", null, new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com" });
        }
    }
}
