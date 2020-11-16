using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlook_Infrastructure.Migrations
{
    public partial class Initialization : Migration
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
                    { new Guid("52140de9-21ce-4f21-8938-e315a56676a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 0 },
                    { new Guid("9f5cfb74-e134-4051-ac02-480b9a7063ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("bd2d1453-534d-4224-abf3-333cc618e465"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "polski@pingwin.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stuart Burton", " + 48696969696" },
                    { new Guid("e5ad9021-aa89-4d3a-98ba-dfaca1965446"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariusz.pudzian@transport.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariusz Pudzianowski", null },
                    { new Guid("8261ebee-5c7a-4c71-8743-46c10dbfe009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrpathix@elo.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Paweł", null },
                    { new Guid("b61fe5a1-ab23-4090-a9f0-7617f8da0848"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nastepne@zawody.fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janne Ahonen", null },
                    { new Guid("54234cda-98bc-41a0-a162-d15d8e20cab6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "papiez_polak@vatican.vc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan Paweł", null },
                    { new Guid("404ce8ee-14be-4490-b5a4-7db489852aeb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kenobi@jedi.order", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obi-Wan Kenobi", null },
                    { new Guid("34931ceb-c487-461a-83d2-a48865e7c4e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "senat@sith.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palpatine", null },
                    { new Guid("971e065d-1877-4f60-be1a-3af198164391"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "plusydodatnie@soli.darnosc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lech Wałęsa", null }
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
