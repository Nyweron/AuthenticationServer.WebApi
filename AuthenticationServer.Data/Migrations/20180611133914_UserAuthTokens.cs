using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthenticationServer.Data.Migrations
{
    public partial class UserAuthTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAuthTokens",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersAuthTokensId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UsersAuthTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAuthTokens", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserAuthTokens",
                table: "Users",
                column: "UserAuthTokens");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersAuthTokens_UserAuthTokens",
                table: "Users",
                column: "UserAuthTokens",
                principalTable: "UsersAuthTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersAuthTokens_UserAuthTokens",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UsersAuthTokens");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserAuthTokens",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserAuthTokens",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UsersAuthTokensId",
                table: "Users");
        }
    }
}
