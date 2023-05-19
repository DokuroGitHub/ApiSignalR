using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAttachment_MessageId",
                table: "MessageAttachment");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Participant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "MessageAttachment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "ConversationInvitation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.UpdateData(
                table: "ConversationInvitation",
                keyColumn: "Id",
                keyValue: 1,
                column: "Role",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ConversationInvitation",
                keyColumn: "Id",
                keyValue: 2,
                column: "Role",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ConversationInvitation",
                keyColumn: "Id",
                keyValue: 3,
                column: "Role",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ConversationInvitation",
                keyColumn: "Id",
                keyValue: 4,
                column: "Role",
                value: 0);

            migrationBuilder.UpdateData(
                table: "MessageAttachment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: 0);

            migrationBuilder.UpdateData(
                table: "MessageAttachment",
                keyColumn: "Id",
                keyValue: 2,
                column: "Type",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Participant",
                keyColumn: "Id",
                keyValue: 3,
                column: "Role",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Participant",
                keyColumn: "Id",
                keyValue: 4,
                column: "Role",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Participant",
                keyColumn: "Id",
                keyValue: 5,
                column: "Role",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAttachment_MessageId",
                table: "MessageAttachment",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAttachment_MessageId",
                table: "MessageAttachment");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Participant",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "MessageAttachment",
                type: "int",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "ConversationInvitation",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ConversationInvitation",
                keyColumn: "Id",
                keyValue: 1,
                column: "Role",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ConversationInvitation",
                keyColumn: "Id",
                keyValue: 2,
                column: "Role",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ConversationInvitation",
                keyColumn: "Id",
                keyValue: 3,
                column: "Role",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ConversationInvitation",
                keyColumn: "Id",
                keyValue: 4,
                column: "Role",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MessageAttachment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MessageAttachment",
                keyColumn: "Id",
                keyValue: 2,
                column: "Type",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Participant",
                keyColumn: "Id",
                keyValue: 3,
                column: "Role",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Participant",
                keyColumn: "Id",
                keyValue: 4,
                column: "Role",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Participant",
                keyColumn: "Id",
                keyValue: 5,
                column: "Role",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAttachment_MessageId",
                table: "MessageAttachment",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id");
        }
    }
}
