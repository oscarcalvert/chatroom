using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatroom.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Offline")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACB3F40C03", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Chatrooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Chatroom__328639197A5AA326", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK__Chatrooms__Creat__286302EC",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PrivateMessages",
                columns: table => new
                {
                    MessageID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PrivateM__C87C037C63C5DC32", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK__PrivateMe__Recei__35BCFE0A",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__PrivateMe__Sende__34C8D9D1",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ChatroomMembers",
                columns: table => new
                {
                    MembershipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    IsModerator = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Chatroom__92A7859924BDD4FD", x => x.MembershipID);
                    table.ForeignKey(
                        name: "FK__ChatroomM__RoomI__31EC6D26",
                        column: x => x.RoomID,
                        principalTable: "Chatrooms",
                        principalColumn: "RoomID");
                    table.ForeignKey(
                        name: "FK__ChatroomM__UserI__30F848ED",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__C87C037C668F28A9", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK__Messages__RoomID__2B3F6F97",
                        column: x => x.RoomID,
                        principalTable: "Chatrooms",
                        principalColumn: "RoomID");
                    table.ForeignKey(
                        name: "FK__Messages__Sender__2C3393D0",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatroomMembers_RoomID",
                table: "ChatroomMembers",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "UQ__Chatroom__94A0AF3CA46232C7",
                table: "ChatroomMembers",
                columns: new[] { "UserID", "RoomID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chatrooms_CreatedBy",
                table: "Chatrooms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomID",
                table: "Messages",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderID",
                table: "Messages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessages_ReceiverID",
                table: "PrivateMessages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessages_SenderID",
                table: "PrivateMessages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E48EEC60A0",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatroomMembers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PrivateMessages");

            migrationBuilder.DropTable(
                name: "Chatrooms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
