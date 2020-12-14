using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlook_Infrastructure.Migrations
{
    public partial class AddAcceptedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailsTo_Users_RecipientId",
                table: "MailsTo");

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

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "Priority" },
                values: new object[,]
                {
                    { new Guid("f9154aea-9350-49df-b60a-4ed58e61e03c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0 },
                    { new Guid("30a5ff06-4fe3-4ad3-a994-49aa505867b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 0 },
                    { new Guid("318ab40b-446a-4144-9fec-af9c5e5bb16d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Accepted", "CreatedDate", "Email", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("6d6a7b2f-d9ce-4929-8672-4f46ef6c7472"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "polski@pingwin.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stuart Burton", " + 48696969696" },
                    { new Guid("318ab40b-446a-4144-9fec-af9c5e5bb16d"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "arturchmura2@op.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Artur Chmura", " + 48696969696" },
                    { new Guid("b8438a0b-a694-4575-9fdc-fe59fc91eff8"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariusz.pudzian@transport.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariusz Pudzianowski", null },
                    { new Guid("cc437d1a-f0d1-4d23-87e8-2794f8500494"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrpathix@elo.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Paweł", null },
                    { new Guid("906bfc2c-c853-408e-90a8-d3c653f9d753"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nastepne@zawody.fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janne Ahonen", null },
                    { new Guid("89f90ee5-dcfc-4959-ad91-57c012a01ae3"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "papiez_polak@vatican.vc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan Paweł", null },
                    { new Guid("c1161710-ddfa-44ed-9584-ab4b527c93e4"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kenobi@jedi.order", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obi-Wan Kenobi", null },
                    { new Guid("5634b7d6-4bf3-481f-8497-1bdfcae2dfcc"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "senat@sith.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palpatine", null },
                    { new Guid("52d88764-bcda-483d-b787-c37c3ef02e5a"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "plusydodatnie@soli.darnosc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lech Wałęsa", null }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("f9154aea-9350-49df-b60a-4ed58e61e03c"), new Guid("318ab40b-446a-4144-9fec-af9c5e5bb16d") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("30a5ff06-4fe3-4ad3-a994-49aa505867b0"), new Guid("318ab40b-446a-4144-9fec-af9c5e5bb16d") });

            migrationBuilder.AddForeignKey(
                name: "FK_MailsTo_Users_RecipientId",
                table: "MailsTo",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailsTo_Users_RecipientId",
                table: "MailsTo");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("318ab40b-446a-4144-9fec-af9c5e5bb16d"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("30a5ff06-4fe3-4ad3-a994-49aa505867b0"), new Guid("318ab40b-446a-4144-9fec-af9c5e5bb16d") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f9154aea-9350-49df-b60a-4ed58e61e03c"), new Guid("318ab40b-446a-4144-9fec-af9c5e5bb16d") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("52d88764-bcda-483d-b787-c37c3ef02e5a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5634b7d6-4bf3-481f-8497-1bdfcae2dfcc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6d6a7b2f-d9ce-4929-8672-4f46ef6c7472"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("89f90ee5-dcfc-4959-ad91-57c012a01ae3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("906bfc2c-c853-408e-90a8-d3c653f9d753"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b8438a0b-a694-4575-9fdc-fe59fc91eff8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1161710-ddfa-44ed-9584-ab4b527c93e4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cc437d1a-f0d1-4d23-87e8-2794f8500494"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("30a5ff06-4fe3-4ad3-a994-49aa505867b0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f9154aea-9350-49df-b60a-4ed58e61e03c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("318ab40b-446a-4144-9fec-af9c5e5bb16d"));

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Users");

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
                name: "FK_MailsTo_Users_RecipientId",
                table: "MailsTo",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
