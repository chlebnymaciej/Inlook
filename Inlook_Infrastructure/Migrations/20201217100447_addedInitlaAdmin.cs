using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlook_Infrastructure.Migrations
{
    public partial class addedInitlaAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ab16ae22-4f23-4e91-bc3c-6420235d12bb"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("28ce0ec9-ff2a-469f-8eb1-7f078c5ad7da"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a9abd872-982c-48a9-8865-e3e5cf5797a2"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22f74dd6-4d63-4d03-8cd4-006b110d840a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2abb0643-366d-4ece-8776-7e2d203f1d20"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3008ae6a-483f-4677-a260-5dbd6f16de3c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("395f1bda-6f88-41a6-8687-a0f9633d4fd0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3a053109-f68d-4f77-9197-ca13257a2860"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66a87f0b-0ebf-4f73-9d0f-c9c17891236f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84b97db8-bd89-4187-85f9-fb64f4047b55"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f812412c-5bd8-4cab-9871-bb598fa9c2ad"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("28ce0ec9-ff2a-469f-8eb1-7f078c5ad7da"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a9abd872-982c-48a9-8865-e3e5cf5797a2"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "Priority" },
                values: new object[,]
                {
                    { new Guid("e4257039-c29d-40ad-b439-db9ff9b72a4e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0 },
                    { new Guid("c16c75cb-fd03-4e5d-9542-d4c511e8a114"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 0 },
                    { new Guid("8b43c219-f27e-47c6-ba7a-263e2e139c6e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Accepted", "CreatedDate", "Email", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("68986989-0c1f-423c-a8da-0408fefd7f70"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "polski@pingwin.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stuart Burton", " + 48696969696" },
                    { new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01142157@pw.edu.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maciej Chlebny", " + 4821372137" },
                    { new Guid("956e80af-7744-4a18-bc9a-ad79ccbcb2b6"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariusz.pudzian@transport.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariusz Pudzianowski", null },
                    { new Guid("ba210bfd-34bc-4519-8674-7ccf956f0f20"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrpathix@elo.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Paweł", null },
                    { new Guid("126e8b28-a5a0-4a8f-b59f-e88a7b6ab366"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nastepne@zawody.fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janne Ahonen", null },
                    { new Guid("c493a508-5ca3-42b5-9306-cb024066920f"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "papiez_polak@vatican.vc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan Paweł", null },
                    { new Guid("e6bc83d7-fb54-4fce-8e95-d030091c3def"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kenobi@jedi.order", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obi-Wan Kenobi", null },
                    { new Guid("327206a0-d20b-4245-82fe-e4b484a3cf11"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "senat@sith.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palpatine", null },
                    { new Guid("6e9bd770-8572-42bc-a8ca-244cabb51354"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "plusydodatnie@soli.darnosc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lech Wałęsa", null }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("e4257039-c29d-40ad-b439-db9ff9b72a4e"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") },
                    { new Guid("c16c75cb-fd03-4e5d-9542-d4c511e8a114"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") },
                    { new Guid("e4257039-c29d-40ad-b439-db9ff9b72a4e"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") },
                    { new Guid("c16c75cb-fd03-4e5d-9542-d4c511e8a114"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8b43c219-f27e-47c6-ba7a-263e2e139c6e"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c16c75cb-fd03-4e5d-9542-d4c511e8a114"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e4257039-c29d-40ad-b439-db9ff9b72a4e"), new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c16c75cb-fd03-4e5d-9542-d4c511e8a114"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e4257039-c29d-40ad-b439-db9ff9b72a4e"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("126e8b28-a5a0-4a8f-b59f-e88a7b6ab366"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("327206a0-d20b-4245-82fe-e4b484a3cf11"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("68986989-0c1f-423c-a8da-0408fefd7f70"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6e9bd770-8572-42bc-a8ca-244cabb51354"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("956e80af-7744-4a18-bc9a-ad79ccbcb2b6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ba210bfd-34bc-4519-8674-7ccf956f0f20"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c493a508-5ca3-42b5-9306-cb024066920f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e6bc83d7-fb54-4fce-8e95-d030091c3def"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c16c75cb-fd03-4e5d-9542-d4c511e8a114"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e4257039-c29d-40ad-b439-db9ff9b72a4e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "Priority" },
                values: new object[,]
                {
                    { new Guid("28ce0ec9-ff2a-469f-8eb1-7f078c5ad7da"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0 },
                    { new Guid("a9abd872-982c-48a9-8865-e3e5cf5797a2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 0 },
                    { new Guid("ab16ae22-4f23-4e91-bc3c-6420235d12bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Accepted", "CreatedDate", "Email", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("f812412c-5bd8-4cab-9871-bb598fa9c2ad"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "polski@pingwin.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stuart Burton", " + 48696969696" },
                    { new Guid("3a053109-f68d-4f77-9197-ca13257a2860"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariusz.pudzian@transport.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariusz Pudzianowski", null },
                    { new Guid("84b97db8-bd89-4187-85f9-fb64f4047b55"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrpathix@elo.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Paweł", null },
                    { new Guid("395f1bda-6f88-41a6-8687-a0f9633d4fd0"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nastepne@zawody.fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janne Ahonen", null },
                    { new Guid("2abb0643-366d-4ece-8776-7e2d203f1d20"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "papiez_polak@vatican.vc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan Paweł", null },
                    { new Guid("66a87f0b-0ebf-4f73-9d0f-c9c17891236f"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kenobi@jedi.order", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obi-Wan Kenobi", null },
                    { new Guid("3008ae6a-483f-4677-a260-5dbd6f16de3c"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "senat@sith.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palpatine", null },
                    { new Guid("22f74dd6-4d63-4d03-8cd4-006b110d840a"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "plusydodatnie@soli.darnosc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lech Wałęsa", null }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("28ce0ec9-ff2a-469f-8eb1-7f078c5ad7da"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a9abd872-982c-48a9-8865-e3e5cf5797a2"), new Guid("2884a694-6a60-4e87-9477-6bd589106ab2") });
        }
    }
}
