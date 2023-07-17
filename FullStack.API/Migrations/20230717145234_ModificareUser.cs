using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullStack.API.Migrations
{
    /// <inheritdoc />
    public partial class ModificareUserNou : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Identities");

            migrationBuilder.RenameColumn(
                name: "provider",
                table: "Identities",
                newName: "Provider");

            migrationBuilder.RenameColumn(
                name: "isSocial",
                table: "Identities",
                newName: "IsSocial");

            migrationBuilder.RenameColumn(
                name: "connection",
                table: "Identities",
                newName: "Connection");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Identities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Identities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Identities",
                table: "Identities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Identities_UserId",
                table: "Identities",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Identities_Users_UserId",
                table: "Identities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identities_Users_UserId",
                table: "Identities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Identities",
                table: "Identities");

            migrationBuilder.DropIndex(
                name: "IX_Identities_UserId",
                table: "Identities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Identities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Identities");

            migrationBuilder.RenameColumn(
                name: "Provider",
                table: "Identities",
                newName: "provider");

            migrationBuilder.RenameColumn(
                name: "IsSocial",
                table: "Identities",
                newName: "isSocial");

            migrationBuilder.RenameColumn(
                name: "Connection",
                table: "Identities",
                newName: "connection");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "Identities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
