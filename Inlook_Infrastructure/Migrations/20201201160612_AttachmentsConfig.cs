using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlook_Infrastructure.Migrations
{
    public partial class AttachmentsConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Mails_MailId",
                table: "Attachments");


            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Attachments",
                newName: "ClientFileName");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MailId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "AzureFileName",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "Priority" },
                values: new object[,]
                {
                    { new Guid("80d9618c-b3ef-4468-90a4-75ef8b131546"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 0 },
                    { new Guid("39b545d0-754c-4ac8-b402-49dca925b655"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("6f26434e-3d52-441b-9f00-832a67906969"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "polski@pingwin.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stuart Burton", " + 48696969696" },
                    { new Guid("b1d96b61-331e-44da-ad18-f7393ff66d48"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariusz.pudzian@transport.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariusz Pudzianowski", null },
                    { new Guid("825bb730-2c00-4ee0-ac67-e0e4e3593131"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrpathix@elo.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Paweł", null },
                    { new Guid("8c1a5b57-092d-4c79-bdd3-f9e86e0d5056"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nastepne@zawody.fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janne Ahonen", null },
                    { new Guid("47664fb2-6c28-4c11-88b3-7cf2e3a88280"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "papiez_polak@vatican.vc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan Paweł", null },
                    { new Guid("2927ce83-7177-43f2-bfc3-8ebf8b53e2aa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kenobi@jedi.order", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obi-Wan Kenobi", null },
                    { new Guid("25b07cda-1692-4876-ba22-13bbb0a81108"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "senat@sith.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palpatine", null },
                    { new Guid("7dd44621-ff22-4f80-bf49-b77b0cd9d436"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "plusydodatnie@soli.darnosc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lech Wałęsa", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Mails_MailId",
                table: "Attachments",
                column: "MailId",
                principalTable: "Mails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Mails_MailId",
                table: "Attachments");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("39b545d0-754c-4ac8-b402-49dca925b655"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("80d9618c-b3ef-4468-90a4-75ef8b131546"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("25b07cda-1692-4876-ba22-13bbb0a81108"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2927ce83-7177-43f2-bfc3-8ebf8b53e2aa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("47664fb2-6c28-4c11-88b3-7cf2e3a88280"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6f26434e-3d52-441b-9f00-832a67906969"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7dd44621-ff22-4f80-bf49-b77b0cd9d436"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("825bb730-2c00-4ee0-ac67-e0e4e3593131"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c1a5b57-092d-4c79-bdd3-f9e86e0d5056"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b1d96b61-331e-44da-ad18-f7393ff66d48"));

            migrationBuilder.DropColumn(
                name: "AzureFileName",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "ClientFileName",
                table: "Attachments",
                newName: "FilePath");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<Guid>(
                name: "MailId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);


            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Mails_MailId",
                table: "Attachments",
                column: "MailId",
                principalTable: "Mails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
