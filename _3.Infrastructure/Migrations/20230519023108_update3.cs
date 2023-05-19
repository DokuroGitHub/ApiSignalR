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
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationBlock_ConversationId",
                table: "ConversationBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationInvitation_ConversationId",
                table: "ConversationInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_DeletedMessage_MessageId",
                table: "DeletedMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_ConversationId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageEmote_MessageId",
                table: "MessageEmote");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_ConversationId",
                table: "Participant");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationBlock_ConversationId",
                table: "ConversationBlock",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationInvitation_ConversationId",
                table: "ConversationInvitation",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeletedMessage_MessageId",
                table: "DeletedMessage",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ConversationId",
                table: "Message",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageEmote_MessageId",
                table: "MessageEmote",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_ConversationId",
                table: "Participant",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationBlock_ConversationId",
                table: "ConversationBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationInvitation_ConversationId",
                table: "ConversationInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_DeletedMessage_MessageId",
                table: "DeletedMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_ConversationId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageEmote_MessageId",
                table: "MessageEmote");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_ConversationId",
                table: "Participant");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationBlock_ConversationId",
                table: "ConversationBlock",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationInvitation_ConversationId",
                table: "ConversationInvitation",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeletedMessage_MessageId",
                table: "DeletedMessage",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ConversationId",
                table: "Message",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageEmote_MessageId",
                table: "MessageEmote",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_ConversationId",
                table: "Participant",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id");
        }
    }
}
