using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthenticationServer.Data.Migrations
{
    public partial class InitAuthenticationServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 40, nullable: false),
                    Login = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 40, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupsPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsPermissions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passwords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pwd = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ResetEmailSent = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    PasswordQuestion = table.Column<string>(maxLength: 100, nullable: false),
                    PasswordAnswer = table.Column<string>(maxLength: 60, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passwords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passwords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersAuthTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    AuthTokenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAuthTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersAuthTokens_AuthTokens_AuthTokenId",
                        column: x => x.AuthTokenId,
                        principalTable: "AuthTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersAuthTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPermissions_GroupId",
                table: "GroupsPermissions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPermissions_PermissionId",
                table: "GroupsPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Passwords_UserId",
                table: "Passwords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAuthTokens_AuthTokenId",
                table: "UsersAuthTokens",
                column: "AuthTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAuthTokens_UserId",
                table: "UsersAuthTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroups_GroupId",
                table: "UsersGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroups_UserId",
                table: "UsersGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsPermissions");

            migrationBuilder.DropTable(
                name: "Passwords");

            migrationBuilder.DropTable(
                name: "UsersAuthTokens");

            migrationBuilder.DropTable(
                name: "UsersGroups");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "AuthTokens");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
