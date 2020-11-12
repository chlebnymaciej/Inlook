using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlook_Infrastructure.Migrations
{
    public partial class Initalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    FavoriteUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.UserId, x.FavoriteUserId });
                    table.ForeignKey(
                        name: "FK_Favorites_Users_FavoriteUserId",
                        column: x => x.FavoriteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    GroupOwnerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_GroupOwnerId",
                        column: x => x.GroupOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    SenderId = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(maxLength: 100, nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mails_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    MailId = table.Column<Guid>(nullable: false),
                    FilePath = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailsTo",
                columns: table => new
                {
                    MailId = table.Column<Guid>(nullable: false),
                    RecipientId = table.Column<Guid>(nullable: false),
                    CC = table.Column<bool>(nullable: false),
                    StatusRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailsTo", x => new { x.MailId, x.RecipientId });
                    table.ForeignKey(
                        name: "FK_MailsTo_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MailsTo_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "Priority" },
                values: new object[,]
                {
                    { new Guid("ac24d94f-9232-4cd5-b9e1-60227f7515b6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 0 },
                    { new Guid("e8a6bc0d-4b7c-4ce3-be0e-8c408cd1b134"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("2c57652f-edea-4648-bbba-130049f47228"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "polski@pingwin.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stuart Burton", " + 48696969696" },
                    { new Guid("5393d49e-9b66-404c-b962-fa2456e40a25"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariusz.pudzian@transport.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariusz Pudzianowski", null },
                    { new Guid("a54d8181-da67-45b7-8202-e1b172f0b366"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrpathix@elo.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Paweł", null },
                    { new Guid("24dcbe92-4975-4d39-b2a6-764c2873d6b5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nastepne@zawody.fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janne Ahonen", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_MailId",
                table: "Attachments",
                column: "MailId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FavoriteUserId",
                table: "Favorites",
                column: "FavoriteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupOwnerId",
                table: "Groups",
                column: "GroupOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mails_SenderId",
                table: "Mails",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MailsTo_RecipientId",
                table: "MailsTo",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "MailsTo");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
