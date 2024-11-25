using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WardrobeOrganizerApp.Migrations
{
    /// <inheritdoc />
    public partial class liy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2a3ddc5e-4419-419b-bb93-4579ca32d35b"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("432161cc-c353-4bc9-8c6b-af3cf4f6169d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("46fe2739-8ed1-4d28-92a0-f1982d4a1da0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("af1f3b26-d83f-4c74-ac23-395beed7f2b2"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("62b2fba0-1b2f-49cc-8b61-b6edcb9e5356"), "customer" },
                    { new Guid("6ec8da4b-8777-44c5-82d7-226ccc1c4f8d"), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { new Guid("40220426-fb55-42a4-863a-cd0c695027f1"), "admin@gmail.com", "$2a$11$y6ikqyZOVVFQyIdvfE0QieKpRsP.TkcA6TfKJah5zfZ754XpgawBi", "$2a$11$y6ikqyZOVVFQyIdvfE0Qie", "Mayokun" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("30902c84-acff-47ab-b2ba-8e066755cde8"), new Guid("6ec8da4b-8777-44c5-82d7-226ccc1c4f8d"), new Guid("40220426-fb55-42a4-863a-cd0c695027f1") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("62b2fba0-1b2f-49cc-8b61-b6edcb9e5356"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("30902c84-acff-47ab-b2ba-8e066755cde8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6ec8da4b-8777-44c5-82d7-226ccc1c4f8d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("40220426-fb55-42a4-863a-cd0c695027f1"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2a3ddc5e-4419-419b-bb93-4579ca32d35b"), "customer" },
                    { new Guid("46fe2739-8ed1-4d28-92a0-f1982d4a1da0"), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { new Guid("af1f3b26-d83f-4c74-ac23-395beed7f2b2"), "admin@gmail.com", "$2a$11$Xwv9jCSzqfvK0V2DnRvy.eS7ZwQy.9JsDYNvSdPD4PtOFPa.y/BXa", "$2a$11$Xwv9jCSzqfvK0V2DnRvy.e", "Mayokun" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("432161cc-c353-4bc9-8c6b-af3cf4f6169d"), new Guid("46fe2739-8ed1-4d28-92a0-f1982d4a1da0"), new Guid("af1f3b26-d83f-4c74-ac23-395beed7f2b2") });
        }
    }
}
