using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlook_Infrastructure.Migrations
{
    public partial class removeAccepted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ac91aade-63ed-4de3-a31b-65109b4789f4"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7537fb2a-9982-40a8-81d3-a5b7cdb8c0d5"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d5ce5076-1d74-4548-81cd-8b01322b39b5"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7537fb2a-9982-40a8-81d3-a5b7cdb8c0d5"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d5ce5076-1d74-4548-81cd-8b01322b39b5"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3c0e4e0d-d91b-42ef-a98e-497177e88597"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("487fed64-8089-46bd-a4a5-5a0a8d26df4d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("53cf29c2-7220-4874-bd05-64ef68ca08a9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7d6f3e44-4ee4-451a-93f2-6b53ab8c5548"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7e0e035f-559b-4ec5-8c79-b8e47b2f0f94"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b4278e66-7d8a-47b3-906d-db95cede49f1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c49ecaac-2fe3-43e1-a1c8-acf9c2fc3ea4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e570f721-96d6-41de-94c7-29ed388c9929"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7537fb2a-9982-40a8-81d3-a5b7cdb8c0d5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d5ce5076-1d74-4548-81cd-8b01322b39b5"));

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "Priority" },
                values: new object[,]
                {
                    { new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0 },
                    { new Guid("2615313f-7df9-49bc-861a-2444abe24dcd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 0 },
                    { new Guid("3ecaed79-15a1-43e8-89c7-3dafc33ae27d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 0 },
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("83fe71d7-503b-4b40-b6b0-9f429394b822"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "polski@pingwin.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stuart Burton", " + 48696969696" },
                    { new Guid("958d9b13-ce78-453c-8287-e7a46720ae85"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariusz.pudzian@transport.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariusz Pudzianowski", null },
                    { new Guid("a2a87bf8-02a3-40e8-a23b-ee199d970264"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrpathix@elo.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Paweł", null },
                    { new Guid("1ad37d42-f44f-4978-8e4f-9e37e95ad860"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nastepne@zawody.fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janne Ahonen", null },
                    { new Guid("76b7b060-58d4-4a79-b3b6-965576f204b4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "papiez_polak@vatican.vc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan Paweł", null },
                    { new Guid("fbddf21d-4f5d-4eb9-9631-bb3d30235f64"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kenobi@jedi.order", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obi-Wan Kenobi", null },
                    { new Guid("d8400a20-7282-4f86-a1e1-1385e8549ee2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "senat@sith.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palpatine", null },
                    { new Guid("c8624a2f-f4cc-4885-8ea6-519cd78418c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "plusydodatnie@soli.darnosc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lech Wałęsa", null },
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") },
                    { new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") },
                    { new Guid("2615313f-7df9-49bc-861a-2444abe24dcd"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") },
                    { new Guid("2615313f-7df9-49bc-861a-2444abe24dcd"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3ecaed79-15a1-43e8-89c7-3dafc33ae27d"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2615313f-7df9-49bc-861a-2444abe24dcd"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2615313f-7df9-49bc-861a-2444abe24dcd"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1ad37d42-f44f-4978-8e4f-9e37e95ad860"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76b7b060-58d4-4a79-b3b6-965576f204b4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("83fe71d7-503b-4b40-b6b0-9f429394b822"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("958d9b13-ce78-453c-8287-e7a46720ae85"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a2a87bf8-02a3-40e8-a23b-ee199d970264"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c8624a2f-f4cc-4885-8ea6-519cd78418c6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d8400a20-7282-4f86-a1e1-1385e8549ee2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fbddf21d-4f5d-4eb9-9631-bb3d30235f64"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2615313f-7df9-49bc-861a-2444abe24dcd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f"));

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
                    { new Guid("7537fb2a-9982-40a8-81d3-a5b7cdb8c0d5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0 },
                    { new Guid("d5ce5076-1d74-4548-81cd-8b01322b39b5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 0 },
                    { new Guid("ac91aade-63ed-4de3-a31b-65109b4789f4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 0 },
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903"),
                column: "Accepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2884a694-6a60-4e87-9477-6bd589106ab2"),
                column: "Accepted",
                value: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Accepted", "CreatedDate", "Email", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("7e0e035f-559b-4ec5-8c79-b8e47b2f0f94"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "polski@pingwin.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stuart Burton", " + 48696969696" },
                    { new Guid("487fed64-8089-46bd-a4a5-5a0a8d26df4d"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariusz.pudzian@transport.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariusz Pudzianowski", null },
                    { new Guid("7d6f3e44-4ee4-451a-93f2-6b53ab8c5548"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrpathix@elo.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Paweł", null },
                    { new Guid("b4278e66-7d8a-47b3-906d-db95cede49f1"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nastepne@zawody.fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janne Ahonen", null },
                    { new Guid("c49ecaac-2fe3-43e1-a1c8-acf9c2fc3ea4"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "papiez_polak@vatican.vc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan Paweł", null },
                    { new Guid("53cf29c2-7220-4874-bd05-64ef68ca08a9"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kenobi@jedi.order", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obi-Wan Kenobi", null },
                    { new Guid("e570f721-96d6-41de-94c7-29ed388c9929"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "senat@sith.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palpatine", null },
                    { new Guid("3c0e4e0d-d91b-42ef-a98e-497177e88597"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "plusydodatnie@soli.darnosc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lech Wałęsa", null },
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("7537fb2a-9982-40a8-81d3-a5b7cdb8c0d5"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") },
                    { new Guid("7537fb2a-9982-40a8-81d3-a5b7cdb8c0d5"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") },
                    { new Guid("d5ce5076-1d74-4548-81cd-8b01322b39b5"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") },
                    { new Guid("d5ce5076-1d74-4548-81cd-8b01322b39b5"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") },
                });
        }
    }
}
