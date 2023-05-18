using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastMessageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation_Id", x => x.Id);
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
                        principalColumn: "Id");
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
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
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
                        principalColumn: "Id");
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participant_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id");
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
                        name: "FK_DeletedMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id");
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
                    Type = table.Column<int>(type: "int", nullable: false, defaultValue: 2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachment_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachment_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageEmote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Code = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageEmote_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageEmote_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Conversation",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "LastMessageId", "PhotoUrl", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, "Nhóm luyện ngục dotnet", null, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", "Nhóm 1", new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Nhóm luyện ngục dotnet v1.1", null, "https://wallpapers.com/images/hd/anime-profile-picture-jioug7q8n43yhlwn.jpg", "Nhóm 1.1", new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Nhóm luyện ngục dotnet v1.3", null, "https://aniyuki.com/wp-content/uploads/2022/03/aniyuki-anime-girl-avatar-51.jpg", "Nhóm 1.3", null, null }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 5, 15, 1, 1, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 6, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ConversationBlock",
                columns: new[] { "ConversationId", "CreatedBy", "UserId", "CreatedAt" },
                values: new object[] { 2, 1, 2, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ConversationInvitation",
                columns: new[] { "Id", "AcceptedAt", "ConversationId", "CreatedAt", "CreatedBy", "JudgedBy", "RejectedAt", "Role", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 2, null, 1, 2 },
                    { 2, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 2, null, 1, 2 },
                    { 3, null, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 3, new DateTime(2022, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 4, null, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, 1, 4 }
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
                columns: new[] { "Id", "ConversationId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Nickname", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, null, null, 1 },
                    { 2, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Admin hiền lành", null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Participant",
                columns: new[] { "Id", "ConversationId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Nickname", "Role", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { 3, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, "Member số 1", 1, null, null, 2 },
                    { 4, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, null, null, 1, null, null, 3 },
                    { 5, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, null, 1, null, null, 4 }
                });

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
                values: new object[] { 1, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", 2, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", 2 });

            migrationBuilder.InsertData(
                table: "MessageEmote",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "MessageId" },
                values: new object[] { 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 2 });

            migrationBuilder.InsertData(
                table: "MessageEmote",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "MessageId" },
                values: new object[] { 2, 2, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 2, 2 });

            migrationBuilder.InsertData(
                table: "MessageAttachment",
                columns: new[] { "Id", "FileUrl", "MessageId", "ThumbUrl" },
                values: new object[] { 2, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg", 3, "https://i.pinimg.com/736x/eb/b4/24/ebb4240e278b99f7ec49a5a51980e187.jpg" });

            migrationBuilder.InsertData(
                table: "MessageEmote",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "MessageId" },
                values: new object[] { 3, 1, new DateTime(2021, 5, 30, 15, 1, 1, 0, DateTimeKind.Unspecified), 1, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_LastMessageId",
                table: "Conversation",
                column: "LastMessageId",
                unique: true,
                filter: "[LastMessageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationInvitation_ConversationId",
                table: "ConversationInvitation",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationId",
                table: "Message",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReplyTo",
                table: "Message",
                column: "ReplyTo");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachment_MessageId",
                table: "MessageAttachment",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEmote_MessageId",
                table: "MessageEmote",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_ConversationId",
                table: "Participant",
                column: "ConversationId");

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
                name: "Message");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 5, 30, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 30, 15, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 5, 30, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 5, 15, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 6, 15, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
