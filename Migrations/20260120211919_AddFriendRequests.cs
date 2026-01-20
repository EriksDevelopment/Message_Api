using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Message_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_UserAId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_UserBId",
                table: "Conversations");

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    RecieverId = table.Column<int>(type: "int", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsDeclined = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_RecieverId",
                table: "FriendRequests",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderId_RecieverId",
                table: "FriendRequests",
                columns: new[] { "SenderId", "RecieverId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_UserAId",
                table: "Conversations",
                column: "UserAId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_UserBId",
                table: "Conversations",
                column: "UserBId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_UserAId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_UserBId",
                table: "Conversations");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_UserAId",
                table: "Conversations",
                column: "UserAId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_UserBId",
                table: "Conversations",
                column: "UserBId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
