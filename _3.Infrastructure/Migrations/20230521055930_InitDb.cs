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
                name: "SampleUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "User"),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(201)", maxLength: 201, nullable: false, computedColumnSql: "\r\nCASE\r\n    WHEN [FirstName] IS NOT NULL AND [FirstName] <> ''\r\n        THEN CASE\r\n  			WHEN [LastName] IS NOT NULL AND [LastName] <> '' \r\n       			THEN [FirstName] + ' ' + [LastName]\r\n       		ELSE [FirstName]\r\n  		END\r\n    WHEN [LastName] IS NOT NULL AND [LastName] <> '' THEN [LastName]\r\n    WHEN [Email] IS NOT NULL AND [Email] <> '' THEN [Email]\r\n	ELSE CAST([Id] AS varchar)\r\nEND", stored: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleUser_Id", x => x.Id);
                    table.CheckConstraint("CK_SampleUser_Money", "[Money] >= 0");
                    table.ForeignKey(
                        name: "FK_SampleUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SampleUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SampleUser_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "SampleUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SampleUser_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "SampleUser",
                        principalColumn: "Id");
                });

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
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "User"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", maxLength: 39, nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", maxLength: 39, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(201)", maxLength: 201, nullable: false, computedColumnSql: "\r\nCASE\r\n    WHEN [FirstName] IS NOT NULL AND [FirstName] <> ''\r\n        THEN CASE\r\n            WHEN [LastName] IS NOT NULL AND [LastName] <> '' \r\n                THEN [FirstName] + ' ' + [LastName]\r\n            ELSE [FirstName]\r\n        END\r\n    WHEN [LastName] IS NOT NULL AND [LastName] <> '' THEN [LastName]\r\n    WHEN [Email] IS NOT NULL AND [Email] <> '' THEN [Email]\r\n    ELSE CAST([Id] AS varchar)\r\nEND", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastMessageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversation_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversation_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Conversation_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConversationBlock",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationBlock_ConversationId_UserId_CreatedBy", x => new { x.ConversationId, x.UserId, x.CreatedBy });
                    table.ForeignKey(
                        name: "FK_ConversationBlock_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationBlock_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConversationBlock_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversationInvitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    JudgedBy = table.Column<int>(type: "int", nullable: true),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationInvitation_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationInvitation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationInvitation_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConversationInvitation_JudgedBy",
                        column: x => x.JudgedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversationInvitation_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplyTo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_ReplyTo",
                        column: x => x.ReplyTo,
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant_ConversationId_UserId", x => new { x.ConversationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Participant_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participant_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participant_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participant_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participant_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeletedMessage",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedMessage_MessageId_CreatedBy", x => new { x.MessageId, x.CreatedBy });
                    table.ForeignKey(
                        name: "FK_DeletedMessage_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeletedMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachment_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachment_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageEmote",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Code = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageEmote_MessageId_UserId", x => new { x.MessageId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MessageEmote_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageEmote_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SampleUser",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "ManagerId", "Money", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com", "Võ", "Thành Đô", null, 1999999999m, "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "Admin", new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "PasswordHash", "Role", "Token", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 1, null, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), null, null, "System", null, "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "Admin", null, null, null, "system" });

            migrationBuilder.InsertData(
                table: "Conversation",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "LastMessageId", "PhotoUrl", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "Nhóm luyện ngục dotnet", null, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", "Nhóm 1", new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Nhóm luyện ngục dotnet v1.1", null, "https://wallpapers.com/images/hd/anime-profile-picture-jioug7q8n43yhlwn.jpg", "Nhóm 1.1", new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Nhóm luyện ngục dotnet v1.3", null, "https://aniyuki.com/wp-content/uploads/2022/03/aniyuki-anime-girl-avatar-51.jpg", "Nhóm 1.3", null, null }
                });

            migrationBuilder.InsertData(
                table: "SampleUser",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "ManagerId", "Money", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 2, new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "dokuro.jp@gmail.com", "Dokuro", "JP", null, 69m, "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "User", new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), null, "dokuro.jp@gmail.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "PasswordHash", "Role", "Token", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[,]
                {
                    { 2, null, new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "dokuro.jp@gmail.com", "Dokuro", "JP", "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "User", null, new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), null, "dokuro.jp@gmail.com" },
                    { 3, null, new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "dovt58@gmail.com", "dovt58", "GG", "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "User", null, new DateTime(2023, 5, 6, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "dovt58@gmail.com" },
                    { 4, null, new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "gidoquenroi1@gmail.com", "gidoquenroi1", "GG", "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "User", null, new DateTime(2023, 5, 6, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "gidoquenroi1@gmail.com" },
                    { 1234567890, null, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com", "Võ", "Thành Đô", "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "Admin", null, new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "tamthoidetrong@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "ConversationBlock",
                columns: new[] { "ConversationId", "CreatedBy", "UserId", "CreatedAt" },
                values: new object[] { 2, 1, 2, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ConversationInvitation",
                columns: new[] { "Id", "AcceptedAt", "ConversationId", "CreatedAt", "CreatedBy", "JudgedBy", "RejectedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 2, null, 2 },
                    { 2, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 2, null, 2 },
                    { 3, null, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 3, new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 3 },
                    { 4, null, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "Id", "Content", "ConversationId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ReplyTo", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, null },
                    { 2, null, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) },
                    { 4, null, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) }
                });

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
                    { 2, 4, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "SampleUser",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FirstName", "LastName", "ManagerId", "Money", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 3, new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "dovt58@gmail.com", "dovt58", "GG", 2, 0m, "$2a$11$2PJMSufmjtIbktnDZ8nbHejByAc9I.wkVQx9u.uzlyye8NhEPMNl6", "User", new DateTime(2023, 5, 6, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "dovt58@gmail.com" });

            migrationBuilder.InsertData(
                table: "DeletedMessage",
                columns: new[] { "CreatedBy", "MessageId", "CreatedAt" },
                values: new object[] { 1, 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "Id", "Content", "ConversationId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ReplyTo", "UpdatedAt" },
                values: new object[] { 3, null, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 2, null, null, 2, null });

            migrationBuilder.InsertData(
                table: "MessageAttachment",
                columns: new[] { "Id", "FileUrl", "MessageId", "ThumbUrl", "Type" },
                values: new object[] { 1, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", 2, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", 1 });

            migrationBuilder.InsertData(
                table: "MessageEmote",
                columns: new[] { "MessageId", "UserId", "UpdatedAt" },
                values: new object[] { 2, 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "MessageEmote",
                columns: new[] { "MessageId", "UserId", "Code", "UpdatedAt" },
                values: new object[] { 2, 2, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "MessageAttachment",
                columns: new[] { "Id", "FileUrl", "MessageId", "ThumbUrl", "Type" },
                values: new object[] { 2, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", 3, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", 1 });

            migrationBuilder.InsertData(
                table: "MessageEmote",
                columns: new[] { "MessageId", "UserId", "Code", "UpdatedAt" },
                values: new object[] { 3, 1, 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_CreatedBy",
                table: "Conversation",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_DeletedBy",
                table: "Conversation",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_LastMessageId",
                table: "Conversation",
                column: "LastMessageId",
                unique: true,
                filter: "[LastMessageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_UpdatedBy",
                table: "Conversation",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationBlock_CreatedBy",
                table: "ConversationBlock",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationBlock_UserId",
                table: "ConversationBlock",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationInvitation_ConversationId",
                table: "ConversationInvitation",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationInvitation_CreatedBy",
                table: "ConversationInvitation",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationInvitation_JudgedBy",
                table: "ConversationInvitation",
                column: "JudgedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationInvitation_UserId",
                table: "ConversationInvitation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedMessage_CreatedBy",
                table: "DeletedMessage",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationId",
                table: "Message",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_CreatedBy",
                table: "Message",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Message_DeletedBy",
                table: "Message",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReplyTo",
                table: "Message",
                column: "ReplyTo");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachment_MessageId",
                table: "MessageAttachment",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEmote_UserId",
                table: "MessageEmote",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_CreatedBy",
                table: "Participant",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_DeletedBy",
                table: "Participant",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_UpdatedBy",
                table: "Participant",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_UserId",
                table: "Participant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleUser_CreatedBy",
                table: "SampleUser",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SampleUser_Email",
                table: "SampleUser",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_SampleUser_ManagerId",
                table: "SampleUser",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleUser_UpdatedBy",
                table: "SampleUser",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SampleUser_Username",
                table: "SampleUser",
                column: "Username",
                unique: true);

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
                name: "IX_User_UpdatedBy",
                table: "User",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserId",
                table: "User",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_LastMessageId",
                table: "Conversation",
                column: "LastMessageId",
                principalTable: "Message",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_CreatedBy",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_DeletedBy",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_UpdatedBy",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_CreatedBy",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_DeletedBy",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_LastMessageId",
                table: "Conversation");

            migrationBuilder.DropTable(
                name: "ConversationBlock");

            migrationBuilder.DropTable(
                name: "ConversationInvitation");

            migrationBuilder.DropTable(
                name: "DeletedMessage");

            migrationBuilder.DropTable(
                name: "MessageAttachment");

            migrationBuilder.DropTable(
                name: "MessageEmote");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "SampleUser");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Conversation");
        }
    }
}
