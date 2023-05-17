using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Colour_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "user"),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "\r\nCASE\r\n    WHEN [FirstName] IS NOT NULL AND [FirstName] <> ''\r\n        THEN CASE\r\n  			WHEN [LastName] IS NOT NULL AND [LastName] <> '' \r\n       			THEN [FirstName] + ' ' + [LastName]\r\n       		ELSE [FirstName]\r\n  		END\r\n    WHEN [LastName] IS NOT NULL AND [LastName] <> '' THEN [LastName]\r\n    WHEN [Email] IS NOT NULL AND [Email] <> '' THEN [Email]\r\n	ELSE CAST([Id] AS varchar)\r\nEND", stored: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Id", x => x.Id);
                    table.CheckConstraint("CK_User_Money", "[Money] >= 0");
                    table.ForeignKey(
                        name: "FK_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Reminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_TodoLists_ListId",
                        column: x => x.ListId,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "ManagerId", "Money", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 1, new DateTime(2021, 5, 30, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com", "Võ", "Thành Đô", null, 1999999999m, "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "admin", new DateTime(2022, 5, 30, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "ManagerId", "Money", "PasswordHash", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 5, 30, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, "dokuro.jp@gmail.com", "Dokuro", "JP", null, 69m, "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", new DateTime(2023, 5, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), null, "dokuro.jp@gmail.com" },
                    { 3, new DateTime(2023, 5, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, "dovt58@gmail.com", "dovt58", "GG", 2, 0m, "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", new DateTime(2023, 5, 6, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, "dovt58@gmail.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ListId",
                table: "TodoItems",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedBy",
                table: "User",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId",
                table: "User",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedBy",
                table: "User",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "TodoLists");
        }
    }
}
